import { Component, OnInit } from '@angular/core';
import { RestaurantDataService } from '../../services/restaurant-data.service';
import { EditorStorage } from './models/editor-storage';
import { RestaurantPlan } from './models/restaurant-plan';
import { EditorBehaviourService } from './services/editor-behaviour.service';
import { EditorDataService } from './services/editor-data.service';
import { EditorSnappingService } from './services/editor-snapping.service';

@Component({
  selector: 'app-restaurant-plan-editor',
  templateUrl: './restaurant-plan-editor.component.html',
  styleUrls: ['./restaurant-plan-editor.component.css'],
  providers: [EditorBehaviourService, EditorSnappingService, EditorDataService]
})
export class RestaurantPlanEditorComponent {

}
