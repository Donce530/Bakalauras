import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimePipe } from './time.pipe';



@NgModule({
  declarations: [TimePipe],
  exports: [TimePipe],
  imports: [
    CommonModule
  ]
})
export class CustomPipesModule { }
