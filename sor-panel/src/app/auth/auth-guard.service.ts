import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router/src/utils/preactivation';
import { AuthService } from './auth.service';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  path: ActivatedRouteSnapshot[];
  route: ActivatedRouteSnapshot;

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): any {
    let roles = route.data['roles'] as Array<string>;
    if (this.authService.willTokenExpireSoon()) {
      this.authService.refreshToken().subscribe((res: any) => {
        localStorage.setItem('token', res.token);
      }
      );
    }
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['login']);
      return false;
    }
    let canActivate = this.authService.checkRoles(roles);
    if (canActivate) {
      return canActivate;
    } else {
      this.router.navigate(['forbidden']);
      return canActivate;
    }
  }
}
