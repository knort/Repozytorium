import { Component, OnInit } from '@angular/core';
import { FoodService } from 'src/app/services/food.service';
import { FoodViewModel } from 'src/app/view-models/food-view-model';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { HighlightService } from 'src/app/shared/highlight.service';

@Component({
  selector: 'app-food',
  templateUrl: './food.component.html',
  styleUrls: ['./food.component.scss']
})
export class FoodComponent implements OnInit {
  foods: FoodViewModel;
  name: FormControl;
  price: FormControl;
  description: FormControl;
  selectedFoodId: string;
  startEditRow: boolean[];
  showCreateInputs: boolean = false;
  param = { value: 'world' }

  constructor(private highlight: HighlightService, private foodService: FoodService, private toastr: ToastrService, private translateSerivce: TranslateService) { }
  ngOnInit() {
    this.highlight.setHighlighted('food');
    let lang = localStorage.getItem('currentlng') !== undefined ? localStorage.getItem('currentlng') : 'pl';
    this.translateSerivce.use(lang);
    this.startEditRow = [];
    this.initalizeFormGroup();
    this.getFood();
  }
  getFood() {
    this.foodService.getAll().subscribe((res: FoodViewModel) => {
      console.log(res);
      this.foods = res
    })
  }
  createInputsToggle() {
    this.showCreateInputs = !this.showCreateInputs;
  }
  cancelCreate() {
    this.createInputsToggle();
    this.initalizeFormGroup();
  }
  initalizeFormGroup() {
    this.name = new FormControl();
    this.price = new FormControl();
    this.description = new FormControl();
  }
  create() {
    const food: FoodViewModel = new FoodViewModel();
    food.name = this.name.value;
    food.description = this.description.value;
    food.price = this.price.value;
    this.foodService.post(food).subscribe(() => { }, err => this.toastr.error(err.error.error), () => {
      this.getFood();
      this.toastr.info('Stworzono przedmiot');
      this.initalizeFormGroup();
      this.createInputsToggle();
    });
  }
  editRow(index: number, foodId: string) {
    this.selectedFoodId = foodId;
    this.startEditRow[index] = true;
    this.name.patchValue(this.foods[index].name);
    this.description.patchValue(this.foods[index].description);
    this.price.patchValue(this.foods[index].price);
  }
  save(index: number) {
    const food = new FoodViewModel();
    food.name = this.name.value;
    food.description = this.description.value;
    food.price = this.price.value;
    food.foodId = this.selectedFoodId;
    this.foodService.put(food).subscribe(() => { }, err => this.toastr.error(err.error.error), () => {
      this.startEditRow[index] = false;
      this.getFood();
      this.toastr.info('Zmodyfikowano przedmiot');
    })
  }
  cancel(index: number) {
    this.initalizeFormGroup();
    this.startEditRow[index] = false;
  }

  delete(foodId) {
    this.foodService.delete(foodId).subscribe(res => { }, err => this.toastr.error(err.error.error), () => {
      this.getFood();
      this.toastr.info('Usunięto przedmiot');
    })
  }

}
