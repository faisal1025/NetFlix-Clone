import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserLoginComponent } from './user-login/user-login.component';
import { UserRegisterComponent } from './user-register/user-register.component';
import { UsersRoutingModule } from './users-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { UserLogoutComponent } from './user-logout/user-logout.component';
import { RevokeSubscriptionComponent } from './revoke-subscription/revoke-subscription.component';



@NgModule({
  declarations: [
    UserLoginComponent,
    UserRegisterComponent,
    UserLogoutComponent,
    RevokeSubscriptionComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    UsersRoutingModule
  ]
})
export class UsersModule { }
