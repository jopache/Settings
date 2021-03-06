import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;
  error: string;

  constructor(private router: Router, private route: ActivatedRoute, private authService: AuthenticationService) { }

  ngOnInit() {
  }

  login(): void {
     this.error = '';
     this.authService.authenticate(this.username, this.password)
     .then(success => {
        if (success) {
          this.router.navigateByUrl('/');
        } else {
          this.error = 'unable to login';
        }
     })
     .catch(exception => {
       this.error = 'invalid username/password';
     });
  }
}
