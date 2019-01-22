import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import * as constants from './constants';
import { AuthGuardService } from './auth/auth-guard.service';
import { ForbiddenComponent } from './forbidden/forbidden.component';

const routes: Routes = [

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadChildren: './auth/auth.module#AuthModule'
  },
  {
    path: 'panel',
    data: { roles: constants.allRoles },
    canActivate: [AuthGuardService],
    loadChildren: './panel/panel.module#PanelModule'
  },
  {
    path: 'forbidden',
    component: ForbiddenComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
