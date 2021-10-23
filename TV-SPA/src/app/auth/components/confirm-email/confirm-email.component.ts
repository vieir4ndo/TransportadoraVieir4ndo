import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    public progressBar: ProgressBarService,
    private alertService: AlertService
  ) { }

  hasError: boolean = false;
  emailConfirmed: boolean = false;
  model: any = {};

  ngOnInit(): void {
    this.model.token = this.route.snapshot.queryParamMap.get('token');
    this.model.userid = this.route.snapshot.queryParamMap.get('userid');
  }

  onSubmit(): void {
    this.alertService.info("Init Confirm Email");
    this.progressBar.startLoading();
    this.authService.confirmEmail(this.model).subscribe(() => {
      console.log("success");
      this.alertService.success("Email Confirmed");
      this.emailConfirmed = true;
      this.progressBar.completeLoading();
    }, error => {
      console.log(error);
      this.alertService.danger("Unable to Confirm Email");
      this.hasError = true;
      this.progressBar.completeLoading();

    });
  }

}
