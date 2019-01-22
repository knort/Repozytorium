import { Injectable, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HighlightService {

  @Output() highlighted:Subject<any> = new Subject();
  constructor() { }

  setHighlighted(number){
    this.highlighted.next(number);
  }
  getHighlighted(){
    return this.highlighted;
  }
}
