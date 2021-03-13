import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantPlanPreviewComponent } from './restaurant-plan-preview.component';

describe('RestaurantPlanPreviewComponent', () => {
  let component: RestaurantPlanPreviewComponent;
  let fixture: ComponentFixture<RestaurantPlanPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RestaurantPlanPreviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RestaurantPlanPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
