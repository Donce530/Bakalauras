import { TestBed } from '@angular/core/testing';

import { ReservationsDataService } from './reservations-data.service';

describe('ReservationsDataService', () => {
  let service: ReservationsDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReservationsDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
