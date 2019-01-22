import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FoodViewModel } from '../view-models/food-view-model';
import { MenuViewModel } from '../view-models/menu-view-model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MenufoodService {
  url = environment.apiUrl + 'menufood';
  constructor(private http: HttpClient) { }

  postMenuFood(menuFood: { menuId: string, foodId: string, food?: FoodViewModel, menu?: MenuViewModel }) {
    return this.http.post(this.url, menuFood);
  }
  deleteMenuFood(menuFoodId: string) {
    return this.http.delete(this.url + '/' + menuFoodId);
  }
}
