import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
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
    this.authService.decodedToken = this.helper.decodeToken(token);
  }
}
