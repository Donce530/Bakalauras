import { Component, Input } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-restaurant-schedule',
  templateUrl: './restaurant-schedule.component.html',
  styleUrls: ['./restaurant-schedule.component.css']
})
export class RestaurantScheduleComponent {

  @Input() edit: boolean;
  @Input() public scheduleFormArray: FormArray;

  public get schedule(): FormGroup[] {
    return this.scheduleFormArray.controls as FormGroup[];
  }
}
