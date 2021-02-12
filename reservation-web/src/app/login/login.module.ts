import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { PrimeNgComponentsModule } from '../shared/modules/prime-ng-components/prime-ng-components.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    PrimeNgComponentsModule,
    FontAwesomeModule,
    ReactiveFormsModule
  ]
})
export class LoginModule { }
