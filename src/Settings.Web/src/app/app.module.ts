import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApplicationInterceptor } from './services/application.interceptor';

import { AppComponent } from './app.component';
import { TreeNodeComponent } from './tree-node/tree-node.component';
import { SettingsViewComponent } from './settings-view/settings-view.component';

import { ApplicationService } from './services/application.service';
import { EnvironmentService } from './services/environment.service';
import { SettingsService } from './services/settings.service';
import { AuthenticationService } from './services/authentication.service';
import { CrudSettingComponent } from './crud-setting/crud-setting.component';

import { AuthGuard } from './guards/auth.guard';



import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SettingsAdminComponent } from './settings-admin/settings-admin.component';
import { UserAdministrationComponent } from './user-administration/user-administration.component';
import { AddUserComponent } from './add-user/add-user.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserService } from './services/user.service';

// todo: Add components
const appRoutes: Routes = [
  { path: '',
    component: SettingsAdminComponent,
    canActivate: [ AuthGuard ]
  },
  { path: 'login',
    component: LoginComponent
  },
  {
    path: 'user-administration',
    component: UserAdministrationComponent,
    canActivate: [AuthGuard]
  }
];


@NgModule({
  declarations: [
    AppComponent,
    TreeNodeComponent,
    SettingsViewComponent,
    CrudSettingComponent,
    LoginComponent,
    SettingsAdminComponent,
    UserAdministrationComponent,
    AddUserComponent,
    UserListComponent
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes,
      // { enableTracing: true } // <-- debugging purposes only
    ),
    BrowserModule,
    HttpModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    ApplicationService,
    EnvironmentService,
    SettingsService,
    AuthenticationService,
    UserService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApplicationInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
