import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextboxControlComponent } from './textbox-control/textbox-control.component';
import { SelectControlComponent } from './select-control/select-control.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DataTableComponent } from './data-table/data-table.component';
import { ResizableColumnDirective } from './directive/resizable-column.directive';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';

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
    DataTableComponent,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule
  ]
})
export class SharedModule { }
