import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Restaurant } from '../models/restaurant';

@Injectable()
export class RestaurantHttpService {
  constructor(private _httpClient: HttpClient) { }

  public GetDetails(): Observable<Restaurant> {
    const url = `${environment.apiUrl}/Restaurant/Details`;

    return this._httpClient.get<Restaurant>(url);
  }

  public SaveDetails(restaurant: Restaurant): Observable<void> {
    const url = `${environment.apiUrl}/Restaurant/Details`;

    return this._httpClient.post<void>(url, restaurant);
  }
}
