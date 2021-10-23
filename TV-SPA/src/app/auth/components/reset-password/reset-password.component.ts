import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  constructor(
    private authService: AuthService,
    public progressBar: ProgressBarService,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
  }

  onSubmit(f: NgForm) {
    this.alertService.info("Init Reset Password");
    this.progressBar.startLoading();
    const resetPasswordObserver = {
      next: () => {
        console.log('Email sent');
        this.alertService.success("Email sent");
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log(err);
        this.alertService.danger("Email not sent");
        this.progressBar.completeLoading();
      }
    };

    this.authService.resetPassword(f.value).subscribe(resetPasswordObserver);
  }
}
