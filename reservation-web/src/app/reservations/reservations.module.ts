import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PrimeNgComponentsModule } from '../shared/modules/prime-ng-components/prime-ng-components.module';
import { CustomPipesModule } from '../shared/pipes/custom-pipes.module';
import { ReservationsComponent } from './reservations.component';
import { ReservationsDataService } from './services/reservations-data.service';
import { ReservationsHttpService } from './services/reservations-http.service';
import { ReservationDetailsComponent } from './reservation-details/reservation-details.component';
import { ReservationDetailsTableComponent } from './reservation-details-table/reservation-details-table.component';
import { ConfirmationService } from 'primeng/api';



@NgModule({
    declarations: [
        ReservationsComponent,
        ReservationDetailsComponent,
        ReservationDetailsTableComponent
    ],
    imports: [
        CommonModule,
        PrimeNgComponentsModule,
        FontAwesomeModule,
        FormsModule,
        CustomPipesModule,
        ReactiveFormsModule
    ],
    providers: [
        ReservationsDataService,
        ReservationsHttpService
    ]
})
export class ReservationsModule { }
