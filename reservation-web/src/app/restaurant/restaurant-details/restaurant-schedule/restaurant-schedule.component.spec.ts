import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantScheduleComponent } from './restaurant-schedule.component';

describe('RestaurantScheduleComponent', () => {
  let component: RestaurantScheduleComponent;
  let fixture: ComponentFixture<RestaurantScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RestaurantScheduleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RestaurantScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
