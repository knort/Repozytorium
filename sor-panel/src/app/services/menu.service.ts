import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  url= environment.apiUrl + 'menu';
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.url);
  }

  getById(id: any) {
    return this.http.get(this.url + '/' + id);
  }

  post(item: any) {
    return this.http.post(this.url, item);
  }

  put(item: any) {
    return this.http.put(this.url, item);
  }

  delete(item: any) {
    return this.http.delete(this.url, item);
  }
}
