import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { TranslateService } from '@ngx-translate/core';
import { HighlightService } from 'src/app/shared/highlight.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  highlight;
  param = { value: 'world' }
  constructor(private highlightService: HighlightService, private router: Router, private route: ActivatedRoute, private authService: AuthService, private translate: TranslateService) { }
  isAdmin: boolean;
  constant = {
    dashboard: 'dashboard',
    reservation: 'reservation',
    table: 'table',
    menu: 'menu',
    food: 'food'
  }
  ngOnInit() {
    this.highlightService.getHighlighted().subscribe(res => {
      this.highlight = res;
    });
    let lang = localStorage.getItem('currentlng') !== undefined ? localStorage.getItem('currentlng') : 'pl';
    this.translate.use(lang);
    this.isAdmin = this.authService.isAdmin();
  }
  logOut() {
    localStorage.removeItem('token');
    this.router.navigate(['']);
  }
  redirect() {
    this.router.navigate(['reservation']);
  }
}
