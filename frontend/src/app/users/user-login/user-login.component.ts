import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { AuthService } from 'src/app/service/AuthService/auth.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit{

  loginForm!:FormGroup
  loginMessage!:String

  constructor(private auth:AuthService, private route: Router) {}

  
  ngOnInit(): void {
    this.loginForm = new FormGroup({
      emailData: new FormControl(null, [Validators.required, Validators.email]),
      passwordData: new FormControl(null, [Validators.required, Validators.minLength(8)])
    });
  }

  onSubmit(){
    console.log(this.loginForm);

    this.auth.loginUser([
      this.loginForm.get('emailData')?.value,
      this.loginForm.get('passwordData')?.value
    ]).subscribe((res)=>{
      console.log(res);
      
      if(res.Code == "true"){
        this.auth.setToken(new String(res.Text));
        this.route.navigateByUrl('');

      }else{
        this.loginMessage = new String(res.Text);
      }
    })
  }




}
