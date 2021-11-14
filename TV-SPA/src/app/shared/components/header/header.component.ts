import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgProgress } from '@ngx-progressbar/core';
import { AlertService } from 'ngx-alerts';
import { AuthService } from '../../services/auth.service';
import { ProgressBarService } from '../../services/progress-bar.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(
    private progress: NgProgress,
    public progressBar: ProgressBarService,
    public authService: AuthService,
    private alertService: AlertService,
    private router : Router
  ) { }

  ngOnInit(): void {
    this.progressBar.progressRef = this.progress.ref("progressBar")
  }

  logout() {
    this.authService.logout();
    this.alertService.success("Logged out");
    this.router.navigate(['']);
  }

}
