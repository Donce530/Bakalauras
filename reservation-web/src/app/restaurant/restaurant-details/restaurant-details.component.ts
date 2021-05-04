import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { faSave } from '@fortawesome/free-regular-svg-icons';
import { faPencilAlt } from '@fortawesome/free-solid-svg-icons';
import { MessageService } from 'primeng/api';
import { OpenHours } from '../models/open-hours';
import { Restaurant } from '../models/restaurant';
import { RestaurantDataService } from '../services/restaurant-data.service';
import { openHoursValidator } from '../validators/restaurant-validators';


@Component({
  selector: 'app-restaurant-details',
  templateUrl: './restaurant-details.component.html',
  styleUrls: ['./restaurant-details.component.css']
})
export class RestaurantDetailsComponent implements OnInit {

  @Input() public set restaurant(restaurant: Restaurant) {
    const scheduleForm = this._formBuilder.array(
      restaurant.schedule.map(oh => this._formBuilder.group({
        weekDay: [oh.weekDay],
        writeableDay: [oh.writeableDay],
        open: [oh.open, Validators.required],
        close: [oh.close, Validators.required]
      },
        { validators: openHoursValidator }))
    );

    this.restaurantForm.patchValue({
      title: restaurant.title,
      description: restaurant.description,
      location: {
        address: restaurant.address,
        city: restaurant.city
      }
    });
    this.restaurantForm.setControl('schedule', scheduleForm);

    this._initialRestaurantValue = restaurant;
  }

  public get scheduleFormArray(): FormArray {
    return this.restaurantForm.get('schedule') as FormArray;
  }
  public get locationFormGroup(): FormGroup {
    return this.restaurantForm.get('location') as FormGroup;
  }

  public restaurantForm: FormGroup;
  public icons = {
    edit: faPencilAlt,
    save: faSave
  };
  public edit = false;

  private _initialRestaurantValue: Restaurant;

  constructor(private _formBuilder: FormBuilder,
    private _dataService: RestaurantDataService,
    private _messageService: MessageService) {
    this.restaurantForm = this._formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      schedule: this._formBuilder.array([]),
      location: this._formBuilder.group({
        address: ['', Validators.required],
        city: ['', Validators.required]
      })
    });
  }

  ngOnInit(): void {
    this._dataService.GetDetails().subscribe(restaurant => {
      this.restaurant = restaurant;
      if (restaurant.title == null) {
        this.toggleEdit();
      }
    });
  }

  public toggleEdit(): void {
    if (this.edit) {
      this.restaurant = this._initialRestaurantValue;
    }
    this.edit = !this.edit;
  }

  public save(): void {
    const formValue = this.restaurantForm.value;
    const timezoneOffset = new Date().getTimezoneOffset() / 60;
    const restaurant = new Restaurant({
      title: formValue.title,
      description: formValue.description,
      schedule: formValue.schedule.map(fs =>
        new OpenHours({
          weekDay: fs.weekDay,
          open: fs.open,
          close: fs.close
        })
      ),
      address: formValue.location.address,
      city: formValue.location.city
    });

    restaurant.schedule.map(oh => {
      oh.open.setUTCHours(oh.open.getHours() + timezoneOffset);
      oh.close.setUTCHours(oh.close.getHours() + timezoneOffset);
      return oh;
    });

    this._dataService.SaveDetails(restaurant).subscribe(
      () => {
        this.edit = !this.edit;
        this.restaurant = restaurant;
        this._messageService.add({ severity: 'success', summary: 'Pavyko!', detail: 'Įstaigos duomenys sėkmingai išsaugoti' })
      }
    );
  }
}
