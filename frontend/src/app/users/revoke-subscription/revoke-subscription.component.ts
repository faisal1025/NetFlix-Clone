import { Component } from '@angular/core';
import { AuthService } from 'src/app/service/AuthService/auth.service';

@Component({
  selector: 'app-revoke-subscription',
  templateUrl: './revoke-subscription.component.html',
  styleUrls: ['./revoke-subscription.component.css']
})
export class RevokeSubscriptionComponent {

  UserList:any;

  constructor(private auth:AuthService) {}

  ngOnInit():void{
    this.getUsers();
  }

  getUsers(){
    this.auth.getAllUsers().subscribe((result)=>{
      console.log(result, "AllUser##");
      
      this.UserList = result.Data;
    });
  }

  onDelete(data:any){
    this.auth.deleteUser(data).subscribe((result)=>{
      alert(result.Text);
      this.getUsers();
    })
  }

}
