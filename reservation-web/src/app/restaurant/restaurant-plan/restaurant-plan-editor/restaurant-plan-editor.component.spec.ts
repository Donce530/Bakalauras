import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantPlanEditorComponent } from './restaurant-plan-editor.component';

describe('RestaurantPlanEditorComponent', () => {
  let component: RestaurantPlanEditorComponent;
  let fixture: ComponentFixture<RestaurantPlanEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RestaurantPlanEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RestaurantPlanEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
