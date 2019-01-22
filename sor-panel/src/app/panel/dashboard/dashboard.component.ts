import { Component, OnInit } from '@angular/core';
import { WordclockService } from 'src/app/services/wordclock.service';
import { ReservationsService } from 'src/app/services/reservations.service';
import { ReservationViewModel } from 'src/app/view-models/reservation-view-model';
import { AuthService } from 'src/app/auth/auth.service';
import { TranslateService } from '@ngx-translate/core';
import { FormControl } from '@angular/forms';
import { ThemeService } from 'src/app/services/theme.service';
import { HighlightService } from 'src/app/shared/highlight.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  param = { value: 'world' }
  language;
  today: any;
  lastReservation: ReservationViewModel;
  userId: string;
  currentUser: any;
  darkTheme;
  constructor(private highlight: HighlightService,private themeService: ThemeService,private wordclockService: WordclockService, private reservationService: ReservationsService, private authService: AuthService, private translateService: TranslateService) { }

  ngOnInit() {
    this.highlight.setHighlighted('dashboard');
    const setDark = localStorage.getItem('dark');
    if( setDark!= undefined && setDark == 'true'){
      this.darkTheme = new FormControl(setDark);
      this.themeService.setTheme(true);
    }else{
      this.darkTheme = new FormControl(false);
    }
    if (localStorage.getItem('currentlng') != undefined) {
      this.language = new FormControl(localStorage.getItem('currentlng'))
    }else{
      localStorage.setItem('currentlng', 'pl');
      this.language = new FormControl('pl');
    }
    this.userId = this.authService.getDecodedToken().nameid;
    this.currentUser = this.authService.getDecodedToken();
    this.wordclockService.getTime().subscribe(res => this.today = res);
    this.reservationService.getLastReservation(this.userId).subscribe((res: ReservationViewModel) => this.lastReservation = res);
  }
  changed() {
    localStorage.setItem('currentlng', this.language.value);
    this.translateService.use(this.language.value);
  }
  changedTheme(){
    localStorage.setItem('dark', this.darkTheme.value);
    console.log(this.darkTheme.value);
    this.themeService.setTheme(this.darkTheme.value);
  }

}
