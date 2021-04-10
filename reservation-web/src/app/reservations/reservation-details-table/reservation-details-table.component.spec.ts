import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationDetailsTableComponent } from './reservation-details-table.component';

describe('ReservationDetailsTableComponent', () => {
  let component: ReservationDetailsTableComponent;
  let fixture: ComponentFixture<ReservationDetailsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReservationDetailsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservationDetailsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
