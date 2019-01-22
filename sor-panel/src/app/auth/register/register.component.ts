import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../auth.service';
import { ToastrComponentlessModule, ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ThemeService } from 'src/app/services/theme.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerGroup: FormGroup;


  constructor(private themeService: ThemeService, private authService: AuthService, private router: Router, private toastr: ToastrService, private translateService: TranslateService) { }

  ngOnInit() {
    const setDark = localStorage.getItem('dark');
    if (setDark != undefined && setDark == 'true')
      this.themeService.setTheme(true);
      else{
        this.themeService.setTheme(false);
      }
    let lang = localStorage.getItem('currentlng') !== undefined ? localStorage.getItem('currentlng') : 'pl';
    this.translateService.use(lang);
    this.registerGroupInit();
  }
  registerGroupInit() {
    this.registerGroup = new FormGroup({
      email: new FormControl(),
      password: new FormControl()
    })
  }
  register() {
    this.authService.register(this.registerGroup.value).subscribe(() => { }, err => {
      if (err.error.Email !== undefined)
        this.toastr.error(err.error.Email)
      if (err.error.password !== undefined) {
        this.toastr.error(err.error.password)
      }
      if (err.error.Password !== undefined) {
        this.toastr.error(err.error.Password)
      }
      console.log(err);
      if (err.error.error !== undefined) {
        this.toastr.error(err.error.error)
      }
    }, () => {
      this.router.navigate(['']);
      this.toastr.info('Pomyślnie zarejestrowano');
    })
  }
  redirect() {
    this.router.navigate(['']);
  }
}
