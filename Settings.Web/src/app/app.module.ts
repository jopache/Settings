import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TreeNodeComponent } from './tree-node/tree-node.component';
import { ApplicationService } from './services/application.service';
import { EnvironmentService } from './services/environment.service';

@NgModule({
  declarations: [
    AppComponent,
    TreeNodeComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule
  ],
  providers: [ApplicationService, EnvironmentService],
  bootstrap: [AppComponent]
})
export class AppModule { }
