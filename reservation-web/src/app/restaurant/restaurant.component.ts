import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RestaurantBehaviourService } from './services/restaurant-behaviour.service';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css']
})
export class RestaurantComponent implements OnInit {

  public editorStateChangeObservable: Observable<boolean>;

  constructor(private _behaviourService: RestaurantBehaviourService) { }

  public ngOnInit(): void {
    this.editorStateChangeObservable = this._behaviourService.editorOpenStateObservable;
  }
}
