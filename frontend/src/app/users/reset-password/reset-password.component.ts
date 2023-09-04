import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/service/AuthService/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  resetPassword!:FormGroup

  constructor(private auth:AuthService) {}
  
  ngOnInit(): void {
    this.resetPassword = new FormGroup({
      password: new FormControl(null, [Validators.required]),
      confirmPassword: new FormControl(null, [Validators.required]),
    });
  }

  onSubmit(){
    console.log(this.resetPassword);
    var email:string = this.resetPassword.value.password;
    this.auth.sendRecoveryEmail(email).subscribe(res=>{
      console.log(res);
      
    })
  }
}
