import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { RestaurantBehaviourService } from '../../services/restaurant-behaviour.service';
import { RestaurantDataService } from '../../services/restaurant-data.service';

@Component({
  selector: 'app-restaurant-plan-preview',
  templateUrl: './restaurant-plan-preview.component.html',
  styleUrls: ['./restaurant-plan-preview.component.css']
})
export class RestaurantPlanPreviewComponent implements OnInit {

  public planPreview: SafeHtml;

  constructor(private _dataService: RestaurantDataService,
    private _behaviourService: RestaurantBehaviourService,
    private _sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this._dataService.getPlanWebPreview().subscribe(
      (plan: string) => this.planPreview = this._sanitizer.bypassSecurityTrustHtml(plan)
    );

  }

  public openEdit(): void {
    this._behaviourService.setEditorOpenState(true);
  }
}
