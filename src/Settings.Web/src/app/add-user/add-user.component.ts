import { Component, OnInit, ViewEncapsulation, ViewChild, Input } from '@angular/core';
import { NgForm } from '@angular/forms';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { User } from '../models/user';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AddUserComponent implements OnInit {
  // todo: review need for NgForm and how NativeScript handles it.
  @ViewChild('addEditUserForm') public addEditUserForm: NgForm;
  username = '';
  isAdmin = false;
  password = '';
  passwordConfirmation = '';
  errorResponse = '';
  response = '';

  constructor(private userService: UserService) { }

  ngOnInit() {
  }

  createUser()  {
    this.response = '';
    this.errorResponse = '';

    if (this.password !== this.passwordConfirmation) {
      this.errorResponse = 'Passwords must match';
      return;
    }

    console.log('user being created: ', this.username, this.isAdmin);
    this.userService.createUser(this.username, this.isAdmin, this.password).then(x => {
      this.response = 'successfully created user: ' + this.username;
      this.username = '';
      this.isAdmin = false;
      this.password = '';
      this.passwordConfirmation = '';
      this.addEditUserForm.form.markAsPristine();
      this.addEditUserForm.form.markAsUntouched();
    })
    .catch(x => {
      // todo: might be good to create proper response types instead of random casting
      const errors = <string[]> x;
      if (errors && errors.length > 0) {
        this.errorResponse = errors[0];
      } else {
        this.errorResponse = `failed to create user: ${this.username}`;
      }
    });
  }

}
