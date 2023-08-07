import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from '../service/AuthService/admin.guard';
import { AuthGuard } from '../service/AuthService/auth.guard';
import { RevokeSubscriptionComponent } from './revoke-subscription/revoke-subscription.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { UserLogoutComponent } from './user-logout/user-logout.component';
import { UserRegisterComponent } from './user-register/user-register.component';

const userRoutes: Routes = [
  {path:"register", component:UserRegisterComponent},
  {path:"login", component:UserLoginComponent},
  {path:"logout", component:UserLogoutComponent, canActivate:[AuthGuard]},
  {path:"revokeSubscription", component:RevokeSubscriptionComponent, canActivate:[AuthGuard, AdminGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(userRoutes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
