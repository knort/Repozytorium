import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PanelRoutingModule } from './panel-routing.module';
import { PanelComponent } from './panel.component';
import { MenuComponent } from './menu/menu.component';
import { ReservationComponent } from './reservation/reservation.component';
import { TableComponent } from './table/table.component';
import { NavbarComponent } from './navbar/navbar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { FoodComponent } from './food/food.component';
import { SharedModule } from '../shared/shared.module';
@NgModule({
  declarations: [PanelComponent, MenuComponent, ReservationComponent, TableComponent, NavbarComponent, DashboardComponent, FoodComponent],
  imports: [
    CommonModule,
    PanelRoutingModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    SharedModule
  ]
})
export class PanelModule { }
