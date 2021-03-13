import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantComponent } from './restaurant.component';
import { RestaurantDetailsComponent } from './restaurant-details/restaurant-details.component';
import { PrimeNgComponentsModule } from '../shared/modules/prime-ng-components/prime-ng-components.module';
import { RestaurantDataService } from './services/restaurant-data.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomPipesModule } from '../shared/pipes/custom-pipes.module';
import { RestaurantTitleComponent } from './restaurant-details/restaurant-title/restaurant-title.component';
import { RestaurantDescriptionComponent } from './restaurant-details/restaurant-description/restaurant-description.component';
import { RestaurantScheduleComponent } from './restaurant-details/restaurant-schedule/restaurant-schedule.component';
import { RestaurantLocationComponent } from './restaurant-details/restaurant-location/restaurant-location.component';
import { RestaurantHttpService } from './services/restaurant-http.service';
import { RestaurantPlanComponent } from './restaurant-plan/restaurant-plan.component';
import { RestaurantPlanPreviewComponent } from './restaurant-plan/restaurant-plan-preview/restaurant-plan-preview.component';
import { RestaurantPlanEditorComponent } from './restaurant-plan/restaurant-plan-editor/restaurant-plan-editor.component';
import { RestaurantBehaviourService } from './services/restaurant-behaviour.service';
import { EditorToolbarComponent } from './restaurant-plan/restaurant-plan-editor/editor-toolbar/editor-toolbar.component';
import { EditorPanelComponent } from './restaurant-plan/restaurant-plan-editor/editor-panel/editor-panel.component';
import { EditorTableOverlayComponent } from './restaurant-plan/restaurant-plan-editor/editor-table-overlay/editor-table-overlay.component';



@NgModule({
  declarations: [RestaurantComponent, RestaurantDetailsComponent, RestaurantTitleComponent, RestaurantDescriptionComponent, RestaurantScheduleComponent, RestaurantLocationComponent, RestaurantPlanComponent, RestaurantPlanPreviewComponent, RestaurantPlanEditorComponent, EditorToolbarComponent, EditorPanelComponent, EditorTableOverlayComponent],
  imports: [
    CommonModule,
    PrimeNgComponentsModule,
    FontAwesomeModule,
    FormsModule,
    CustomPipesModule,
    ReactiveFormsModule
  ],
  providers: [
    RestaurantDataService,
    RestaurantHttpService,
    RestaurantBehaviourService
  ]
})
export class RestaurantModule { }
