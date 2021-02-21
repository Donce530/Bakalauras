import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantComponent } from './restaurant.component';
import { RestaurantDetailsComponent } from './restaurant-details/restaurant-details.component';
import { RestaurantEditorComponent } from './restaurant-editor/restaurant-editor.component';
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



@NgModule({
  declarations: [RestaurantComponent, RestaurantDetailsComponent, RestaurantEditorComponent, RestaurantTitleComponent, RestaurantDescriptionComponent, RestaurantScheduleComponent, RestaurantLocationComponent],
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
    RestaurantHttpService
  ]
})
export class RestaurantModule { }
