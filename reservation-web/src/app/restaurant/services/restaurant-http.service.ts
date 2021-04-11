import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Restaurant } from '../models/restaurant';
import { RestaurantPlan } from '../restaurant-plan/restaurant-plan-editor/models/restaurant-plan';

@Injectable()
export class RestaurantHttpService {
  private _detailsUrl = `${environment.apiUrl}/Restaurant/Details`;
  private _planUrl = `${environment.apiUrl}/Restaurant/Plan`;

  constructor(private _httpClient: HttpClient) { }

  public GetDetails(): Observable<Restaurant> {
    return this._httpClient.get<Restaurant>(this._detailsUrl);
  }

  public SaveDetails(restaurant: Restaurant): Observable<void> {
    return this._httpClient.post<void>(this._detailsUrl, restaurant);
  }

  public savePlan(plan: RestaurantPlan): Observable<void> {
    return this._httpClient.post<void>(this._planUrl, plan);
  }

  public getPlan(): Observable<RestaurantPlan> {
    return this._httpClient.get<RestaurantPlan>(this._planUrl);
  }

  public getPlanWebPreview(): Observable<string> {
    const url = `${this._planUrl}/Preview`;

    return this._httpClient.get(url, { responseType: 'text' });
  }

  public getQrCode(): Observable<Blob> {
    const url = `$${environment.apiUrl}/Restaurant/QR`;

    return this._httpClient.get<Blob>(url);
  }
}
