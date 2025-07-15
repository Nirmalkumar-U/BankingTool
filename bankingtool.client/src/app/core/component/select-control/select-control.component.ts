import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DropDownDto } from '../../../dto/drop-down-dto';

@Component({
  selector: 'app-select-control',
  templateUrl: './select-control.component.html',
  styleUrls: ['./select-control.component.css']
})
export class SelectControlComponent {
  @Input() label: string = '';
  @Input() options: DropDownDto[] = [];
  @Input() control!: FormControl;
  @Input() required: boolean = false;
}
