import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthViewModel } from '../view-models/authviewmodel';
import { HttpClient } from '@angular/common/http';
import * as jwt_decode from 'jwt-decode'
import * as constants from '../constants';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  url = environment.apiUrl + "auth";
  constructor(private http: HttpClient) { }

  login(authViewModel: AuthViewModel) {
    return this.http.post(this.url, authViewModel);
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }
  isAuthenticated() {
    return this.getDecodedToken() !== undefined && (!this.isTokenExpired());
  }

  isTokenExpired() {
    return new Date().getTime() / 1000 > this.getDecodedToken().exp
  }

  willTokenExpireSoon() {
    const currentTime = new Date().getTime() / 1000;
    const expireTime = this.getDecodedToken().exp;
    return expireTime - currentTime < 560;
  }

  refreshToken() {
    const jwtViewModel = {
      token: this.getToken()
    }
    return this.http.post(this.url + '/refresh', jwtViewModel);
  }
  getToken() {
    return localStorage.getItem('token');
  }
  isAdmin() {
    const decodedToken = this.getDecodedToken();
    if (decodedToken.role instanceof Array) {
      for (let i = 0; i < decodedToken.role.length; i++) {
        if (decodedToken.role[i] === constants.adminRole[0])
          return true;
      }
      return false;
    } else
      return decodedToken.role.includes(constants.adminRole);
  }
  getDecodedToken() {
    const token = localStorage.getItem('token');
    let decodedToken;
    if (token !== null)
      decodedToken = jwt_decode(token);
    return decodedToken;
  }
  checkRoles(allowedRoles: string[]): boolean {
    const decodedToken = this.getDecodedToken();
    if (decodedToken.role instanceof Array) {
      for (let i = 0; i < decodedToken.role.length; i++) {
        if (allowedRoles.includes(decodedToken.role[i]))
          return true;
      }
      return false;
    }
    else {
      return allowedRoles.includes(decodedToken.role);
    }
  }

  register(user) {
    return this.http.post(this.url + '/register', user);
  }
}
