import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TreeNodeComponent } from './tree-node/tree-node.component';
import { SettingsViewComponent } from './settings-view/settings-view.component';

import { ApplicationService } from './services/application.service';
import { EnvironmentService } from './services/environment.service';
import { SettingsService } from './services/settings.service';


@NgModule({
  declarations: [
    AppComponent,
    TreeNodeComponent,
    SettingsViewComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule
  ],
  providers: [ApplicationService, EnvironmentService, SettingsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
