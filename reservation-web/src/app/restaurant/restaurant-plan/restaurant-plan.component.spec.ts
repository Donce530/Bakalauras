import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantPlanComponent } from './restaurant-plan.component';

describe('RestaurantPlanComponent', () => {
  let component: RestaurantPlanComponent;
  let fixture: ComponentFixture<RestaurantPlanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RestaurantPlanComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RestaurantPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
