import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private baseUrl: string = 'http://localhost:4000/'; 
  userLoggedIn!: Observable<boolean>; // An observable to subscribe in need on other components
  observableLoginChange = new BehaviorSubject<boolean>(false); // Local subject variable to pass next calls
  constructor(private http: HttpClient) {
    this.userLoggedIn = this.observableLoginChange.asObservable();
  }

  signUp(userObject:any){
    const userRequest = {
      Role: "User",
      Token: "",
      LastName: userObject.lastName,
      UserName: userObject.username,
      FirstName: userObject.firstName,
      email: userObject.email,
      password: userObject.password
    }
    return this.http.post<any>(`${this.baseUrl}signup`, userRequest);
  }

  login(loginObj:any){
    const userRequest = {
      // Role: "",
      // Token: "",
      // LastName: "",
      // UserName: loginObj.username,
      // FirstName: "",
      email: loginObj.email,
      password: loginObj.password
    }
    return this.http.post<any>(`${this.baseUrl}users/login`, userRequest);
  }

  onLoginUser(loginStatus: boolean) { // A method used to pass values into this service from source
    this.observableLoginChange.next(loginStatus);
  }




  sendOTP(email:string):Observable<any>{
    return this.http.post<any>(`${this.baseUrl}sendOTP`,{email})
  }
  verifyOTP(email:any,code:string):Observable<any>{
    return this.http.post<any>(`${this.baseUrl}verifyOTP`,{email,code})
  }
}
