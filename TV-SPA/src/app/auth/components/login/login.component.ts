import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService,
    public progressBar: ProgressBarService,
    private alertService: AlertService,
    private router : Router
  ) { }

  ngOnInit(): void {

  }

  onSubmit(f: NgForm) {
    this.alertService.info('Checking Information');
    this.progressBar.startLoading();
    this.progressBar.startLoading();
    const loginObserver = {
      next: () => {
        this.alertService.success('User Logged In');
        this.progressBar.completeLoading();
        this.router.navigate(['/home']);
      },
      error: (err: any) => {
        console.log(err);
        this.alertService.danger('Unable to Login');
        this.progressBar.completeLoading();
      }
    };

    this.authService.login(f.value).subscribe(loginObserver);
  }
}
