import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextboxControlComponent } from './textbox-control/textbox-control.component';
import { SelectControlComponent } from './select-control/select-control.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DataTableComponent } from './data-table/data-table.component';
import { ResizableColumnDirective } from './directive/resizable-column.directive';



@NgModule({
  declarations: [
    TextboxControlComponent,
    SelectControlComponent,
    DataTableComponent,
    ResizableColumnDirective
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    TextboxControlComponent,
    SelectControlComponent,
    DataTableComponent
  ]
})
export class SharedModule { }
