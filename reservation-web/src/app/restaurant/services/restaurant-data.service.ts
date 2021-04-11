import { WeekDay } from '@angular/common';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { OpenHours } from '../models/open-hours';
import { Restaurant } from '../models/restaurant';
import { Table } from '../restaurant-plan/restaurant-plan-editor/drawables/table';
import { Wall } from '../restaurant-plan/restaurant-plan-editor/drawables/wall';
import { EditorStorage } from '../restaurant-plan/restaurant-plan-editor/models/editor-storage';
import { RestaurantPlan } from '../restaurant-plan/restaurant-plan-editor/models/restaurant-plan';
import { RestaurantHttpService } from './restaurant-http.service';

@Injectable()
export class RestaurantDataService {
  constructor(private _httpService: RestaurantHttpService) { }

  private plan: RestaurantPlan;

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

  public savePlan(plan: RestaurantPlan): Observable<void> {
    for (const table of plan.tables) {
      table.linkedTableNumbers = table.linkedTables.map(t => t.number);
      table.linkedTables = [];
    }
    return this._httpService.savePlan(plan);
  }

  public getPlan(): Observable<RestaurantPlan> {
    return this._httpService.getPlan().pipe(
      map(plan => {
        if (plan == null) {
          return new RestaurantPlan(0, '', new EditorStorage());
        } else {
          const mappedPlan = new RestaurantPlan(plan.id, plan.webSvg,
            new EditorStorage({
              tables: plan.tables.map(t => new Table(t)),
              walls: plan.walls.map(w => new Wall(w))
            }));

          mappedPlan.tables.forEach(table => {
            table.linkedTables = mappedPlan.tables.filter(t => table.linkedTableNumbers.includes(t.number));
          });

          return mappedPlan;
        }
      })
    );
  }

  public getPlanWebPreview(): Observable<string> {
    return this._httpService.getPlanWebPreview().pipe(
      map(svg => svg == null ? '' : svg)
    );
  }

  public getQrCode(): Observable<Blob> {
    return this._httpService.getQrCode();
  }
}
