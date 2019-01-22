import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WordclockService {

  
  constructor(private http: HttpClient) { }
  getTime(){
    return this.http.get(environment.worldClockApiUrl);
  }
}
