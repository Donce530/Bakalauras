import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedResponse } from '../../shared/models/data/paged-response';
import { ReservationDataRow } from '../models/reservation-data-row';
import { ReservationDetails } from '../models/reservation-details';
import { ReservationsRequest } from '../models/reservations-request';
import { ReservationsHttpService } from './reservations-http.service';

@Injectable()
export class ReservationsDataService {
  constructor(private _httpService: ReservationsHttpService) { }

  public getReservations(request: ReservationsRequest): Observable<PagedResponse<ReservationDataRow>> {
    return this._httpService.getReservations(request);
  }

  public getDetails(id: number): Observable<ReservationDetails> {
    return this._httpService.getDetails(id);
  }
}
