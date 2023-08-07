import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/service/AuthService/auth.service';

@Component({
  selector: 'app-user-logout',
  templateUrl: './user-logout.component.html',
  styleUrls: ['./user-logout.component.css']
})
export class UserLogoutComponent {

  constructor(private auth:AuthService, private router:Router) {}

  ngOnInit():void{
    this.LogOut();
  }

  LogOut(){
    this.auth.logOut();
    this.router.navigateByUrl("");
  }
}
