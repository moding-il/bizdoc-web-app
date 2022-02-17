import { Component, ChangeDetectorRef, Injector } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ComponentPortal, PortalInjector } from '@angular/cdk/portal';
import { OverlayRef, Overlay } from '@angular/cdk/overlay';
import { FormComponent, RecipientModel, CubeEntry, ViewMode, BizDoc } from '@bizdoc/core';
import { MyFormLineComponent, LINE_TOKEN } from './my-form-line/my-form-line.component';

@Component({
  templateUrl: './my-form.component.html',
  styleUrls: ['./my-form.component.scss']
})
@BizDoc({
  selector: 'app-my-form'
})
/** myForm component*/
export class MyFormComponent implements FormComponent<MyFormModel>{
  mode: ViewMode;
  form = this.fb.group({
    subject: this.fb.control([], [Validators.required])
  });
  model: MyFormModel;
  displayedColumns = ['value', 'actions'];
  dataSource: MatTableDataSource<LineModel>;
  cube: CubeEntry;
  onBind(data: RecipientModel<MyFormModel>) {
    this.model = data.model;
    this.dataSource = new MatTableDataSource(this.model.lines);
    this.cube = data.cubes[0];
  }
  addLine() {
    this.newLine({});
  }
  remove(line: LineModel) {
    this.model.lines.remove(line);
  }
  copy(line: LineModel) {
    const newLine: LineModel = { ...line };
    this.newLine(newLine);
  }
  private newLine(line: any) {
    this.dialog.open(MyFormLineComponent, line).afterClosed().subscribe(e => {
      if (e) {
        this.model.lines.push(e);
        this.changeDetectorRef.detectChanges();
      }
    });
  }
  edit(line: LineModel) {
    const oRef = this.overlay.create({
      positionStrategy: this.overlay.position().global().centerHorizontally().centerVertically(),
      backdropClass: 'cdk-overlay-transparent-backdrop',
      hasBackdrop: true
    });
    const injectionTokens = new WeakMap();
    injectionTokens.set(OverlayRef, oRef);
    injectionTokens.set(LINE_TOKEN, line);
    const injector = new PortalInjector(this.injector, injectionTokens);
    const portal = new ComponentPortal(MyFormLineComponent, null, injector);
    oRef.attach(portal);
    oRef.backdropClick().subscribe(() => oRef.detach());
  }
  /** myForm ctor */
  constructor(private dialog: MatDialog, private fb: FormBuilder, private changeDetectorRef: ChangeDetectorRef, private overlay: Overlay, private injector: Injector) {
  }
}

export interface MyFormModel {
  subject: string;
  lines: LineModel[];
}
export interface LineModel {
  value?: number;
}
