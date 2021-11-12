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
    this.alertService.info("Checking Information");
    this.progressBar.startLoading();
    const resetPasswordObserver = {
      next: () => {
        this.alertService.success("New Password Solicitated");
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log(err);
        this.alertService.danger("Unable to Solicitate New Password");
        this.progressBar.completeLoading();
      }
    };

    this.authService.resetPassword(f.value).subscribe(resetPasswordObserver);
  }
}
