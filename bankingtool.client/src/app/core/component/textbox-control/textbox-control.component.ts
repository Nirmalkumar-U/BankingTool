import { Component, Input } from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';

@Component({
  selector: 'app-textbox-control',
  templateUrl: './textbox-control.component.html',
  styleUrls: ['./textbox-control.component.css']
})
export class TextboxControlComponent {
  @Input() label: string = '';
  @Input() type: string = 'text'; // 'text', 'number', 'date', etc.
  @Input() control!: AbstractControl | null;
  @Input() required: boolean = false;

  get formControl(): FormControl {
    return this.control as FormControl;
  }
}
