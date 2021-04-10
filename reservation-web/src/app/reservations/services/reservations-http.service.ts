import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PagedResponse } from '../models/paged-response';
import { ReservationDataRow } from '../models/reservation-data-row';
import { ReservationDetails } from '../models/reservation-details';
import { ReservationsRequest } from '../models/reservations-request';

@Injectable()
export class ReservationsHttpService {
  constructor(private _httpClient: HttpClient) { }

  getReservations(request: ReservationsRequest): Observable<PagedResponse<ReservationDataRow>> {
    const url = `${environment.apiUrl}/Reservation/PagedAndFiltered`;

    return this._httpClient.post<PagedResponse<ReservationDataRow>>(url, request);
  }

  public getDetails(id: number): Observable<ReservationDetails> {
    const url = `${environment.apiUrl}/Reservation/Details/${id}`;

    return this._httpClient.get<ReservationDetails>(url);
  }
}
