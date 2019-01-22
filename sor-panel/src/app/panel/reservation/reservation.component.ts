import { Component, OnInit } from '@angular/core';
import { ReservationViewModel } from 'src/app/view-models/reservation-view-model';
import { ReservationsService } from 'src/app/services/reservations.service';
import { TableService } from 'src/app/services/table.service';
import { TableViewModel } from 'src/app/view-models/table-view-model';
import { FormGroup, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { TranslateService } from '@ngx-translate/core';
import { HighlightService } from 'src/app/shared/highlight.service';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit {
  tables: TableViewModel[];
  inputs: boolean = false;
  reservationGroup: FormGroup;
  selectedTable: TableViewModel;
  reservations: ReservationViewModel[];
  allReservations: ReservationViewModel[];
  userReservations: ReservationViewModel[];
  isAdmin: any;
  manage: boolean = false;
  param = { value: 'world' }
  constructor(private highlight: HighlightService, private translate: TranslateService, private reservationService: ReservationsService, private tableService: TableService, private toastr: ToastrService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.highlight.setHighlighted('reservation');
    let lang = localStorage.getItem('currentlng') !== undefined ? localStorage.getItem('currentlng') : 'pl';
    this.translate.use(lang);
    this.initFormGroup();
    this.isAdmin = this.authService.isAdmin();
    this.tableService.getAll().subscribe((res: TableViewModel[]) => this.tables = res);
    this.reservationService.getUserReservations(this.authService.getDecodedToken().nameid).subscribe((res: ReservationViewModel[]) => {
      this.userReservations = res
      this.userReservations.sort((a, b) => this.sortByDate(a, b))
    }
    );
  }

  initFormGroup() {
    this.reservationGroup = new FormGroup({
      date: new FormControl(),
      start: new FormControl(),
      end: new FormControl()
    });
  }

  toggleManage() {
    this.manage = !this.manage;
  }
  changeToManage() {
    this.toggleManage();
    this.getAllReservations();
  }
  getAllReservations(): any {
    this.reservationService.getAll().subscribe((res: ReservationViewModel[]) => {
      this.allReservations = res;
    });
  }

  deleteReservation(reservationId: string) {
    this.reservationService.delete(reservationId).subscribe(res => { }, err => this.toastr.error(err.error.error), () => {
      this.toastr.info('Usunięto rezerwację');
      this.toggleManage();
    })
  }

  toggleInputs() {
    this.inputs = !this.inputs;
  }
  startReservation(table: TableViewModel) {
    this.selectedTable = table;
    this.toggleInputs();
  }
  createReservation() {
    const reservation = new ReservationViewModel();
    reservation.tableId = this.selectedTable.tableId;
    reservation.date = this.reservationGroup.value.date;
    reservation.userId = this.authService.getDecodedToken().nameid;
    if (this.reservationGroup.value.date === null) {
      this.toastr.error('Musisz wybrać dzień z kalendarza zanim spróbujesz zarezerwować stolik');
    }
    if (this.reservationGroup.value.start === null) {
      this.toastr.error('Musisz wypełnić godzinę startu');
    }
    if (this.reservationGroup.value.end === null) {
      this.toastr.error('Musisz wypełnić godzinę końca');
    }
    else {
      reservation.start = this.convertDate(this.reservationGroup.value.start, reservation.date);
      reservation.end = this.convertDate(this.reservationGroup.value.end, reservation.date);
      this.reservationService.post(reservation).subscribe(() => { }, err => {
        if (err.error.end != undefined) {
          this.toastr.error(err.error.end);
        }
        if (err.error.start != undefined) {
          this.toastr.error(err.error.start);
        } else {
          this.toastr.error(err.error.error);
        }
      }, () => {
        this.toastr.info('Stworzono rezerwację');
        this.router.navigate(['panel']);
      });
    }
  }

  datePicked() {
    this.reservationService.getTableReservations(this.selectedTable.tableId, this.reservationGroup.value.date).subscribe((res: ReservationViewModel[]) => {
      this.reservations = res;
      this.reservations = this.reservations.sort((a, b) => this.sortByDate(a, b));
    }
    );
  }
  sortByDate(a: any, b: any) {
    if (a.start > b.start)
      return 1;
    if (a.start < b.start)
      return -1;
    return 0;
  }
  convertDate(time: string, date: Date): any {
    let dateAsString = date.toLocaleString();
    let convertedDate = dateAsString.split('.').reverse().join('-');
    convertedDate += 'T' + time;
    return convertedDate;
  }


}
