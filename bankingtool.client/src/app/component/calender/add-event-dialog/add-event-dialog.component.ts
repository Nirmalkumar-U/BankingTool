import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-event-dialog',
  templateUrl: './add-event-dialog.component.html',
  styleUrls: ['./add-event-dialog.component.css']
})
export class AddEventDialogComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddEventDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { date: string }
  ) {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      startDateTime: [this.data.date, Validators.required],
      endDateTime: [this.data.date],
      eventType: ['Event', Validators.required],
      isAllDay: [true]
    });
  }

  save() {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  close() {
    this.dialogRef.close();
  }
}
