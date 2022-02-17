import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BizDocModule } from '@bizdoc/core';
import { CredentialsModule } from '@bizdoc/credentials';
import { AppComponent } from './app.component';
import { MyFormComponent } from './my-form/my-form.component';
import { MyReportComponent } from './my-report/my-report.component';
import { MyFormLineComponent } from './my-form/my-form-line/my-form-line.component';

@NgModule({
  declarations: [
    AppComponent,
    MyFormComponent,
    MyFormLineComponent,
    MyReportComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BizDocModule.forRoot({
      components: [MyFormComponent, MyReportComponent]
    }),
    CredentialsModule.forRoot()
  ],
  providers: [],
  entryComponents: [MyFormLineComponent],
  bootstrap: [AppComponent]
})
export class CustomModule {
}
