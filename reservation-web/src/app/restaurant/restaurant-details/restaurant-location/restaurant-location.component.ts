import { Component, Input } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-restaurant-location',
  templateUrl: './restaurant-location.component.html',
  styleUrls: ['./restaurant-location.component.css']
})
export class RestaurantLocationComponent {

  @Input() edit: boolean;
  @Input() locationFormGroup: FormGroup;
}
