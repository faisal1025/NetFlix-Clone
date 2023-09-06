import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { catchError } from 'rxjs';
import { AuthService } from 'src/app/service/AuthService/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  resetPassword!:FormGroup
  token!:string
  uid!:string
  baseUrl = 'https://localhost:5001/api/User';

  constructor(private auth:AuthService, private router: ActivatedRoute, private http: HttpClient) {}
  
  ngOnInit(): void {
    this.resetPassword = new FormGroup({
      password: new FormControl(null, [Validators.required, Validators.minLength(8)]),
      confirmPassword: new FormControl(null, [Validators.required, Validators.minLength(8)]),
    }, this.passwordMatchedValidator('password', 'confirmPassword'));
  }

  passwordMatchedValidator(controlNameA: string, controlNameB: string):ValidatorFn{
    return(control: AbstractControl): ValidationErrors | null => {
      const formGroup = control as FormGroup;
      const password = formGroup.get(controlNameA)?.value;
      const confpassword = formGroup.get(controlNameB)?.value;
      return password === confpassword ? null: {matched: false};
    }
  }

  
  onSubmit(){
    this.router.queryParams.subscribe(res=>{
      this.token = res['token'];
      this.uid = res['uid']      
    })
    // const uid = this.router.snapshot.queryParamMap.get('uid');
    console.log(this.resetPassword);
    var password:string = this.resetPassword.value.password;
    var confirmpassword:string = this.resetPassword.value.password;
    var formData: FormData = new FormData();
    formData.append('Password', password);
    formData.append('Confirmpassword', confirmpassword);
    formData.append('Uid', this.uid);
    this.auth.sendResetPassword(formData, this.token)
    .pipe(
      catchError((err) => {
        alert(err.error.text)
        console.error(err); // Log the error for debugging
        throw err; // Rethrow the error to propagate it further
      })
    )
    .subscribe(
      (res) => {
        console.log(res);
      }
    );
  }
}
