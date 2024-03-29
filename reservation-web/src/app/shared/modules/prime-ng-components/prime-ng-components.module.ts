import { NgModule } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DividerModule } from 'primeng/divider';
import { TooltipModule } from 'primeng/tooltip';
import { MenubarModule } from 'primeng/menubar';
import { FieldsetModule } from 'primeng/fieldset';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table';
import { GalleriaModule } from 'primeng/galleria';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ToolbarModule } from 'primeng/toolbar';
import { SelectButtonModule } from 'primeng/selectbutton';
import { SliderModule } from 'primeng/slider';
import { CheckboxModule } from 'primeng/checkbox';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { InputNumberModule } from 'primeng/inputnumber';
import { TabMenuModule } from 'primeng/tabmenu';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';

@NgModule({
  exports: [
    CardModule,
    ButtonModule,
    InputTextModule,
    DividerModule,
    TooltipModule,
    MenubarModule,
    FieldsetModule,
    CalendarModule,
    TableModule,
    GalleriaModule,
    ScrollPanelModule,
    InputTextareaModule,
    ToolbarModule,
    SelectButtonModule,
    SliderModule,
    CheckboxModule,
    OverlayPanelModule,
    InputNumberModule,
    TabMenuModule,
    ProgressSpinnerModule,
    DropdownModule,
    ConfirmDialogModule,
    DialogModule,
    ToastModule
  ]
})
export class PrimeNgComponentsModule { }
