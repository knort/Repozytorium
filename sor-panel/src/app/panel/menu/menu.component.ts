import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { FoodService } from 'src/app/services/food.service';
import { FoodViewModel } from 'src/app/view-models/food-view-model';
import { MenuViewModel } from 'src/app/view-models/menu-view-model';
import { MenuService } from 'src/app/services/menu.service';
import { ToastrService } from 'ngx-toastr';
import { MenufoodService } from 'src/app/services/menufood.service';
import { AuthService } from 'src/app/auth/auth.service';
import { TranslateService } from '@ngx-translate/core';
import { HighlightService } from 'src/app/shared/highlight.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  inputsToggle: boolean = false;
  foods: FoodViewModel[];
  menuFormGroup: FormGroup;
  menu: MenuViewModel;
  displayNoProductToAdd: boolean = false;
  isAdmin: boolean;
  param = { value: 'world' }


  constructor(private highlight: HighlightService, private translateService: TranslateService, private foodService: FoodService, private menuService: MenuService, private toastr: ToastrService, private menufoodService: MenufoodService, private authSerivce: AuthService) { }

  ngOnInit() {
    this.highlight.setHighlighted('menu');
    let lang = localStorage.getItem('currentlng') !== undefined ? localStorage.getItem('currentlng') : 'pl';
    this.translateService.use(lang);
    this.initFormGroup();
    this.getMenu();
    this.isAdmin = this.authSerivce.isAdmin();
  }
  getMenu() {
    this.menuService.getAll().subscribe((res: MenuViewModel) => {
      this.menu = res[0];
      this.foodService.getFoodForMenu(this.menu.menuId).subscribe((res: FoodViewModel[]) => {
        this.displayNoProductToAdd = res.length > 0;
        this.foods = res
      });
    });
  }
  initFormGroup() {
    this.menuFormGroup = new FormGroup({
      food: new FormControl()
    })
  }
  showInputs() {
    this.inputsToggle = !this.inputsToggle;
  }
  addFood() {
    const menu = { ...this.menu };
    menu.menuFoods = null;
    this.menufoodService.postMenuFood({ foodId: this.menuFormGroup.value.food.foodId, menuId: this.menu.menuId }).subscribe(res => { }, err => console.log(err), () => {
      this.showInputs();
      this.getMenu();
      this.toastr.info('Dodano produkt do menu');
    });
  }
  deleteProduct(menuFoodId: string){
    this.menufoodService.deleteMenuFood(menuFoodId).subscribe(res => { }, err => this.toastr.error(err.error.error), () => {
      this.getMenu();
    })
  }
}
