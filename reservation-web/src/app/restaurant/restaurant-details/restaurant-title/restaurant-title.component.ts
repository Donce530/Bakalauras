import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';

@Component({
  selector: 'app-restaurant-title',
  templateUrl: './restaurant-title.component.html',
  styleUrls: ['./restaurant-title.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => RestaurantTitleComponent),
    multi: true
  }]
})
export class RestaurantTitleComponent implements ControlValueAccessor, OnInit {

  @Input() edit: boolean;
  public titleControlGroup: FormGroup;

  constructor(private _formBuilder: FormBuilder) { }

  writeValue(value: string): void {
    this.titleControlGroup.patchValue({
      title: value
    });
  }

  registerOnChange(fn: any): void {
    this.titleControlGroup.controls.title.valueChanges.subscribe(fn);
  }

  registerOnTouched(fn: any): void { }

  ngOnInit(): void {
    this.titleControlGroup = this._formBuilder.group({
      title: ['', Validators.required]
    });
  }
}
