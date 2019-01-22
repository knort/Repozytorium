import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReservationsService {

  url = environment.apiUrl + 'reservation';
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.url);
  }

  getById(id: any) {
    return this.http.get(this.url + '/' + id);
  }

  getTableReservations(id: string, date: Date) {
    return this.http.post(this.url + `/tableReservations`, { tableId: id, date: date });
  }

  getUserReservations(id: string) {
    return this.http.get(this.url + '/userReservations/' + id);
  }

  getLastReservation(userId: string) {
    return this.http.get(this.url + '/last/' + userId);
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
