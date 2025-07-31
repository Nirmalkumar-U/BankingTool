import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { DataTableComponent } from './data-table/data-table.component';
import { ResizableColumnDirective } from './directive/resizable-column.directive';
import { SelectControlComponent } from './select-control/select-control.component';
import { TextboxControlComponent } from './textbox-control/textbox-control.component';

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
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule,
    TextboxControlComponent,
    SelectControlComponent,
    DataTableComponent,
    ResizableColumnDirective
  ]
})
export class SharedModule { }
