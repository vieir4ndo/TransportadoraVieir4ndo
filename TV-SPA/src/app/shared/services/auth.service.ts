import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  authUrl = "http://localhost:5000/api/auth/";
  usersUrl = "http://localhost:5000/api/users/";
  confirmEmailUrl = "http://localhost:4200/confirm-email";
  changePasswordUrl = "http://localhost:4200/change-password";
  helper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.authUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user.result.succeeded) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.helper.decodeToken(user.token);
        }
      })
    )
  }

  registerUser(model: any, type: string) {
    let headers = new HttpHeaders({
      'confirmEmailUrl': this.confirmEmailUrl
    });
    let options = { headers: headers };
    return this.http.post(this.usersUrl + 'create-' + type, model, options);
  }

  resetPassword(model: any) {
    let headers = new HttpHeaders({
      'ChangePasswordUrl': this.changePasswordUrl
    });
    let options = { headers: headers };
    return this.http.post(this.authUrl + 'reset-password', model, options);
  }

  confirmEmail(model: any) {
    return this.http.post(this.authUrl + 'confirm-email', model);
  }

  changePassword(model: any) {
    return this.http.post(this.authUrl + 'change-password', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token') as string;
    return !this.helper.isTokenExpired(token);
  }
}
