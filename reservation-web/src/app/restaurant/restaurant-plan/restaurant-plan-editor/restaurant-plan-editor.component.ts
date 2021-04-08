import { Component } from '@angular/core';
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
