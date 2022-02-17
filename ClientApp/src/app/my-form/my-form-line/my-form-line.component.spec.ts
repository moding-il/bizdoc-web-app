/// <reference path="../../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { MyFormLineComponent } from './my-form-line.component';

let component: MyFormLineComponent;
let fixture: ComponentFixture<MyFormLineComponent>;

describe('my-form-line component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ MyFormLineComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(MyFormLineComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});