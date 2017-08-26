import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CrudSettingComponent } from './crud-setting.component';

describe('CrudSettingComponent', () => {
  let component: CrudSettingComponent;
  let fixture: ComponentFixture<CrudSettingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CrudSettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CrudSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
