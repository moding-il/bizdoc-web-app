import { Component, Inject, OnInit, InjectionToken } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LineModel } from '../my-form.component';

export const LINE_TOKEN = new InjectionToken<LineModel>('line');

@Component({
  selector: 'app-my-form-line',
  templateUrl: './my-form-line.component.html',
  styleUrls: ['./my-form-line.component.scss']
})
/** my-form-line component*/
export class MyFormLineComponent implements OnInit {
  form = this.fb.group({
    value: this.fb.control([], [Validators.required])
  });/** my-form-line ctor */
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) private data: {}, private dialogRef: MatDialogRef<MyFormLineComponent>) {
  }
  ngOnInit(): void {
    this.form.patchValue(this.data);
  }
  close() {
    this.dialogRef.close();
  }
  ok() {
    this.dialogRef.close(this.form.value);
  }
}
