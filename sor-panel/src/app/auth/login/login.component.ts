import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { ThemeService } from 'src/app/services/theme.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginFormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });
  param = { value: 'world' }
  constructor(private themeService: ThemeService, private authService: AuthService, private router: Router, private translateService: TranslateService, private toastr: ToastrService) { }

  ngOnInit() {
    const setDark = localStorage.getItem('dark');
    if (setDark != undefined && setDark == 'true')
      this.themeService.setTheme(true);
    else{
      this.themeService.setTheme(false);
    }
    let lang = localStorage.getItem('currentlng') !== undefined ? localStorage.getItem('currentlng') : 'pl';
    this.translateService.use(lang);
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['panel']);
    }
  }

  login() {
    this.authService.login(this.loginFormGroup.value).subscribe((res: { token: string }) => {
      this.authService.setToken(res.token);
    }, err => {
      const error = err.error;
      if (error.Password !== undefined) {
        this.toastr.error(error.Password);
      }
      if (error.Email !== undefined) {
        this.toastr.error(error.Email);
      }
      if (error.error != undefined) {
        this.toastr.error(error.error);
      }
    }, () => {
      this.toastr.info('Pomyślnie zalogowano');
      this.router.navigate(['panel']);
    });
  }

}
