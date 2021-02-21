import { WeekDay } from '@angular/common';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { OpenHours } from '../models/open-hours';
import { Restaurant } from '../models/restaurant';
import { RestaurantHttpService } from './restaurant-http.service';

@Injectable()
export class RestaurantDataService {

  constructor(private _httpService: RestaurantHttpService) { }

  public GetDetails(): Observable<Restaurant> {
    return this._httpService.GetDetails().pipe(map<Restaurant, Restaurant>(
      (r: Restaurant) => {
        if (r == null) {
          r = new Restaurant({
            schedule: OpenHours.defaultSchedule()
          });
        } else {
          r.schedule = r.schedule.map(oh => {
            const mappedHours = new OpenHours({
              open: new Date(oh.open),
              close: new Date(oh.close),
              weekDay: oh.weekDay
            });

            mappedHours.open.setUTCHours(mappedHours.open.getHours());
            mappedHours.close.setUTCHours(mappedHours.close.getHours());

            return mappedHours;
          });

          r.schedule.sort((a, b) => {
            return a < b ? -1 : 1;
          });
          r.schedule.push(r.schedule.shift());
        }

        return r;
      }));
  }

  public SaveDetails(restaurant: Restaurant): Observable<void> {
    return this._httpService.SaveDetails(restaurant);
  }
}
