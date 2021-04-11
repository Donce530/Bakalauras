import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantQrComponent } from './restaurant-qr.component';

describe('RestaurantQrComponent', () => {
  let component: RestaurantQrComponent;
  let fixture: ComponentFixture<RestaurantQrComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RestaurantQrComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RestaurantQrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
