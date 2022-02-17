import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { MyReportComponent } from './my-report.component';

let component: MyReportComponent;
let fixture: ComponentFixture<MyReportComponent>;

describe('my-report component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ MyReportComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(MyReportComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
