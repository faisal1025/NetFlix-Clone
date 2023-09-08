import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/service/AuthService/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit{
  forgotPassword!:FormGroup

  constructor(private auth:AuthService) {}
  
  ngOnInit(): void {
    this.forgotPassword = new FormGroup({
      emailData: new FormControl(null, [Validators.required, Validators.email]),
    });
  }

  onSubmit(){
    console.log(this.forgotPassword);
    var email:string = this.forgotPassword.value.emailData;
    this.auth.sendRecoveryEmail(email).subscribe(res=>{
      console.log(res);
      alert(res.Text)
    })
  }

}
