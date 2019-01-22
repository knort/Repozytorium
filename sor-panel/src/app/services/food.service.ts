import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  url = environment.apiUrl + 'food';
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.url);
  }

  getFoodForMenu(menuId: string) {
    return this.http.get(this.url + '/foodForMenu/' + menuId);
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
    return this.http.delete(this.url + '/' + item);
  }
}
