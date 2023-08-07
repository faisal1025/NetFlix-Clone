import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { BehaviorSubject, Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  currentUser:BehaviorSubject<any> = new BehaviorSubject(null);
  userDetails:any;
  constructor(private http:HttpClient) { }

  baseUrl = 'https://localhost:5001/api/User';

  jwtHelperService = new JwtHelperService();

  addUser(user:Array<String>) : Observable<any>{
    console.log(user, '#data');
    
    return this.http.post(`${this.baseUrl}/register`, {
      UserName: user[0],
      UserEmail: user[1],
      Password: user[2]
    });
  }

  loginUser(user:Array<String>) : Observable<any>{
    console.log(user, 'loginData#');
    
    return this.http.post(`${this.baseUrl}/login`, {
      UserEmail: user[0],
      Password: user[1]
    });
  }

  setToken(token: any) {
    localStorage.setItem("tokenAccess", token);
    this.getCurrentUser();
  }

  getCurrentUser(){
    const token = localStorage.getItem("tokenAccess");
    const deCryptedToken = token == null ? null : this.jwtHelperService.decodeToken(token);
    const data = deCryptedToken?{
      Id : deCryptedToken.id,
      UserName : deCryptedToken.name,
      UserEmail : deCryptedToken.email,
      Role : deCryptedToken.role
    }:null;
    this.currentUser.next(data);
  }

  isLoggedIn():boolean{
    return localStorage.getItem("tokenAccess") ? true : false;
  }

  isAdmin():boolean{
    return this.currentUser.value.Role == "Admin";
  }

  logOut(){
    localStorage.removeItem("tokenAccess");
  }

  getAllUsers():Observable<any>{
    return this.http.get(`${this.baseUrl}/getUsers`);
  }

  deleteUser(data:any) : Observable<any>{
    return this.http.delete(`${this.baseUrl}/delete/${data}`);
  }
}
