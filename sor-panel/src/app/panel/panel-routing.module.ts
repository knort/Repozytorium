import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PanelComponent } from './panel.component';
import { MenuComponent } from './menu/menu.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ReservationComponent } from './reservation/reservation.component';
import { TableComponent } from './table/table.component';
import { FoodComponent } from './food/food.component';
import * as constants from '../constants';
import { AuthGuardService } from '../auth/auth-guard.service';

const routes: Routes =
  [
    {
      path: '',
      component: PanelComponent,
      children: [
        {
          path: '',
          component: DashboardComponent,
          canActivate: [AuthGuardService],
          data: { roles: constants.allRoles },
        },
        {
          path: 'menu',
          component: MenuComponent,
          canActivate: [AuthGuardService],
          data: { roles: constants.allRoles },

        },
        {
          path: 'reservation',
          component: ReservationComponent,
          canActivate: [AuthGuardService],
          data: { roles: constants.allRoles },
        },
        {
          path: 'table',
          component: TableComponent,
          canActivate: [AuthGuardService],
          data: { roles: constants.adminRole }
        },
        {
          path: 'food',
          component: FoodComponent,
          canActivate: [AuthGuardService],
          data: { roles: constants.adminRole }
        }
      ]
    }
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PanelRoutingModule { }
