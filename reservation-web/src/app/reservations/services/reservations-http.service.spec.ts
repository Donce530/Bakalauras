import { TestBed } from '@angular/core/testing';

import { ReservationsHttpService } from './reservations-http.service';

describe('ReservationsHttpService', () => {
  let service: ReservationsHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReservationsHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
