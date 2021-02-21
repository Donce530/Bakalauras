import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';

@Component({
  selector: 'app-restaurant-description',
  templateUrl: './restaurant-description.component.html',
  styleUrls: ['./restaurant-description.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => RestaurantDescriptionComponent),
    multi: true
  }]
})
export class RestaurantDescriptionComponent implements OnInit, ControlValueAccessor {

  @Input() edit: boolean;
  public descriptionControlGroup: FormGroup;

  constructor(private _formBuilder: FormBuilder) { }

  writeValue(value: string): void {
    this.descriptionControlGroup.patchValue({
      description: value
    });
  }

  registerOnChange(fn: any): void {
    this.descriptionControlGroup.controls.description.valueChanges.subscribe(fn);
  }

  registerOnTouched(fn: any): void { }

  ngOnInit(): void {
    this.descriptionControlGroup = this._formBuilder.group({
      description: ['', Validators.required]
    });
  }
}
