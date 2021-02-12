import { NgModule } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DividerModule } from 'primeng/divider';
import { TooltipModule } from 'primeng/tooltip';



@NgModule({
  exports: [
    CardModule,
    ButtonModule,
    InputTextModule,
    DividerModule,
    TooltipModule
  ]
})
export class PrimeNgComponentsModule { }
