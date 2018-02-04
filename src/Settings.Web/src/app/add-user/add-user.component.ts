import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../services/user.service';

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

  constructor(private userService: UserService) { }

  ngOnInit() {
  }

  createUser()  {
    console.log('user being created: ', this.username, this.isAdmin);
    this.userService.createUser(this.username, this.isAdmin).then(x => {
      this.username = '';
      this.isAdmin = false;
      this.addEditUserForm.form.markAsPristine();
      this.addEditUserForm.form.markAsUntouched();
    })
    .catch(x => {
      console.log('failure creating user', x);
    });
  }

}
