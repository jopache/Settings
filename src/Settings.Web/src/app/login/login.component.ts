import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthorizationService } from '../services/authorization.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;
  error: string;

  constructor(private router: Router, private route: ActivatedRoute, private authService: AuthorizationService) { }

  ngOnInit() {
  }

  login(): void {
     console.log(this.router, this.route, this.authService);
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
