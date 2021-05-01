import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserManagerComponent } from './user-manager.component';
import { PrimeNgComponentsModule } from '../shared/modules/prime-ng-components/prime-ng-components.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CustomPipesModule } from '../shared/pipes/custom-pipes.module';
import { UserManagerHttpService } from './services/user-manager-http.service';
import { UserManagerDataService } from './services/user-manager-data.service';
import { ConfirmationService } from 'primeng/api';
import { NewManagerDialogComponent } from './new-manager-dialog/new-manager-dialog.component';



@NgModule({
  declarations: [UserManagerComponent, NewManagerDialogComponent],
  imports: [
    CommonModule,
    PrimeNgComponentsModule,
    FontAwesomeModule,
    FormsModule,
    CustomPipesModule,
    ReactiveFormsModule
  ],
  providers: [
    UserManagerDataService,
    UserManagerHttpService,
    ConfirmationService
  ]
})
export class UserManagerModule { }
