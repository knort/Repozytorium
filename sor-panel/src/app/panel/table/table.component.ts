import { Component, OnInit } from '@angular/core';
import { TableViewModel } from 'src/app/view-models/table-view-model';
import { TableService } from 'src/app/services/table.service';
import { FormGroup, FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { HighlightService } from 'src/app/shared/highlight.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {

  param = { value: 'world' }
  tables: TableViewModel[];
  tableFormGroup: FormGroup;
  addingTable: boolean = false;
  editingTable: boolean = false;
  selectedTable: TableViewModel;
  constructor(private highlight: HighlightService, private tableService: TableService, private translate: TranslateService, private toastr: ToastrService) { }

  ngOnInit() {
    this.highlight.setHighlighted('table');
    let lang = localStorage.getItem('currentlng') !== undefined ? localStorage.getItem('currentlng') : 'pl';
    this.translate.use(lang);
    this.initFormGroup();
    this.getTables();
  }
  getTables(): any {
    this.tableService.getAll().subscribe((tables: TableViewModel[]) => {
      this.tables = tables;
    });
  }
  toggleAddingTable() {
    this.addingTable = !this.addingTable;
  }
  addTable() {
    let table = new TableViewModel();
    table = { ...this.tableFormGroup.value };
    this.tableService.post(table).subscribe(res => { }, err => console.log(err), () => {
      this.getTables();
      this.toastr.info('Dodano stolik');
      this.toggleAddingTable();
    });
  }
  initFormGroup() {
    this.tableFormGroup = new FormGroup({
      number: new FormControl()
    });
  }
  cancel(value?: any) {
    if (value !== undefined) {
      this.initFormGroup();
      this.toggleEditingTable();
      this.selectedTable=undefined;
    } else {
      this.toggleAddingTable();
    }
  }
  toggleEditingTable(): any {
    this.editingTable = !this.editingTable;
  }
  startEditing(id: number) {
    this.tableService.getById(id).subscribe((res: TableViewModel) => {
      this.selectedTable = res;
      this.tableFormGroup.patchValue({
        number: this.selectedTable.number
      });
    });
    this.toggleEditingTable();
  }
  deleteTable() {
    this.tableService.delete(this.selectedTable.tableId).subscribe(res => {}, err => { }, () => {
      this.getTables();
      this.toggleEditingTable();
      this.initFormGroup();
      this.toastr.info('Usunięto stolik');
      this.selectedTable=undefined;
    });
  }
  modifyTable() {
    const table = this.selectedTable;
    table.number = this.tableFormGroup.value.number;
    this.tableService.put(table).subscribe(res => { }, err => { }, () => {
      this.getTables();
      this.toggleEditingTable();
      this.initFormGroup();
      this.toastr.info('Zmodyfikowano numer stolika');
      this.selectedTable=undefined;
    });
  }
}
