import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, SubscriptionLike } from 'rxjs';
import { RestaurantBehaviourService } from '../services/restaurant-behaviour.service';

@Component({
  selector: 'app-restaurant-plan',
  templateUrl: './restaurant-plan.component.html',
  styleUrls: ['./restaurant-plan.component.css']
})
export class RestaurantPlanComponent implements OnInit {

  public editorOpenStateObservable: Observable<boolean>;
  public edit = false;

  constructor(private _behaviourService: RestaurantBehaviourService) { }

  ngOnInit(): void {
    this.editorOpenStateObservable = this._behaviourService.editorOpenStateObservable;
  }
}
