import { Injectable } from '@angular/core';
import { Table } from '../drawables/table';
import { EditorStorage } from '../models/editor-storage';
import { RestaurantPlan } from '../models/restaurant-plan';

@Injectable()
export class EditorDataService {

  public set storage(storage: EditorStorage) {
    this._storage = storage;
  }

  public get storage(): EditorStorage {
    return this._storage;
  }

  public get plan(): RestaurantPlan {
    return new RestaurantPlan(this.planId, this._svg, this._storage);
  }

  public set svg(unstyledSvg: string) {
    this._svg = [unstyledSvg.slice(0, 97), this._hardcodedSvgStyle, unstyledSvg.slice(97)].join('');
  }

  public planId = 0;
  private _svg: string;
  private _storage: EditorStorage;

  private _hardcodedSvgStyle = ' style="position: absolute;fill-rule:evenodd;display: block;max-width: 100%;max-height: 100%;left: 0;right: 0;top: 0;bottom: 0;margin: auto;" viewBox="0 0 3840 2160"';

  constructor() { }
}
