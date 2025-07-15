import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextboxControlComponent } from './textbox-control/textbox-control.component';
import { SelectControlComponent } from './select-control/select-control.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    TextboxControlComponent,
    SelectControlComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    TextboxControlComponent,
    SelectControlComponent
  ]
})
export class SharedModule { }
