import { Component, OnInit } from '@angular/core';
import { TreeNode } from './treenode';
import { ApplicationService } from './services/application.service';
import { EnvironmentService } from './services/environment.service';
import { SettingsService } from './services/settings.service';
import { Observable } from 'rxjs/Observable';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(public applicationService: ApplicationService, public environmentService: EnvironmentService) {



  }

  ngOnInit(): void {
    
  }
}
