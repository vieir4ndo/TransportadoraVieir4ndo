import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './shared/models/user';
import { AuthService } from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  helper = new JwtHelperService();

  constructor(
    private authService: AuthService
  ) { }

  ngOnInit() {
    const token = localStorage.getItem('token') as string;
    const user: User = JSON.parse(localStorage.getItem('user') as string);
    
    if (token){
      this.authService.decodedToken = this.helper.decodeToken(token); 
    }
    
    if (user){
      this.authService.currentUser = user;
      this.authService.validateAvatar();
    }
  }
}
