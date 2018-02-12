import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserListComponent implements OnInit {

  constructor(private userService: UserService) { }

  users: User[] = null;

  ngOnInit() {
    this.userService.loadUsers()
      .then(users => {
        this.users = users;
      })
      .catch(error => {
        console.log('error loading users', error);
      });
  }

}
