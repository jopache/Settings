import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEdditUserComponent } from './add-eddit-user.component';

describe('AddEdditUserComponent', () => {
  let component: AddEdditUserComponent;
  let fixture: ComponentFixture<AddEdditUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddEdditUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEdditUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
