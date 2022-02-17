import { Component } from '@angular/core';
import { ReportComponent, BizDoc } from '@bizdoc/core';

@Component({
  templateUrl: './my-report.component.html',
  styleUrls: ['./my-report.component.scss']
})
@BizDoc({
  selector: 'app-my-report'
})
/** my-report component*/
export class MyReportComponent implements ReportComponent<MyReportDataModel, MyReportArgsModel> {
  data: MyReportDataModel[];
  onBind(result: MyReportDataModel[]) {
    this.data = result;
  }
  /** my-report ctor */
  constructor() {
  }
}
interface MyReportArgsModel {
}
interface MyReportDataModel {
  column1: string;
}
