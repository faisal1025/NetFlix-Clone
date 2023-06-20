import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validator, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/service/AuthService/auth.service';
import { User } from '../user-model';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit{

  constructor(private auth:AuthService, private router:Router) {}
  signupForm!:FormGroup
  IsRegisteredMessage!:String

  ngOnInit(): void {
    this.signupForm = new FormGroup({
      'nameData': new FormControl(null, [Validators.required]),
      'emailData': new FormControl(null, [Validators.required, Validators.email]),
      'passwordData': new FormControl(null, [Validators.required, Validators.minLength(8)]),
      'confirmPasswordData': new FormControl(null, [Validators.required, Validators.minLength(8)]),
      'checkData': new FormControl(false, [Validators.requiredTrue])
    }, this.passwordMatchValidators('passwordData', 'confirmPasswordData'))
  }
  

  passwordMatchValidators(controlNameA: string, controlNameB : string):ValidatorFn{
    return (control: AbstractControl): ValidationErrors | null =>{
      const formGroup = control as FormGroup;
      const password = formGroup.get('passwordData')?.value;
      const confpassword = formGroup.get('confirmPasswordData')?.value;
      return password === confpassword ? null : {matched:false};
    }
  }

  onSubmit(){
    console.log(this.signupForm);
    this.auth.addUser([
      this.signupForm.get('nameData')?.value,
      this.signupForm.get('emailData')?.value,
      this.signupForm.get('passwordData')?.value
    ]).subscribe((res)=>{
      console.log(res, "result#");
      
      if(res.IsSuccess){
        this.IsRegisteredMessage = res.MainMessage.Text;
        alert(this.IsRegisteredMessage);
        this.router.navigateByUrl('login');
      }else{
        this.IsRegisteredMessage = res.MainMessage.Text;
        alert(this.IsRegisteredMessage);
      }
      
    })
    
  }
}
