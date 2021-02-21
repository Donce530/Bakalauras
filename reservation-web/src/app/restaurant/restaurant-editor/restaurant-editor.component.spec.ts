import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantEditorComponent } from './restaurant-editor.component';

describe('RestaurantEditorComponent', () => {
  let component: RestaurantEditorComponent;
  let fixture: ComponentFixture<RestaurantEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RestaurantEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RestaurantEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
