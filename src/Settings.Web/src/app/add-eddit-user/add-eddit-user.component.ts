import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-eddit-user',
  templateUrl: './add-eddit-user.component.html',
  styleUrls: ['./add-eddit-user.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AddEdditUserComponent implements OnInit {
  @ViewChild('addEditUserForm') public addEditUserForm: NgForm;
  username: String = '';
  isAdmin: Boolean = false;
  constructor() { }

  ngOnInit() {
  }

  createUser()  {
    console.log('user being created: ', this.username, this.isAdmin);
    this.username = '';
    this.isAdmin = false;
    this.addEditUserForm.form.markAsPristine();
    this.addEditUserForm.form.markAsUntouched();
  }

}
