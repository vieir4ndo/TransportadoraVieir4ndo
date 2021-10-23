import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    public progressBar: ProgressBarService,
    private alertService: AlertService
  ) { }

  model: any = {};
  changedPassword: boolean = false;
  hasError: boolean = false;

  ngOnInit(): void {
    this.model.token = this.route.snapshot.queryParamMap.get('token');
    this.model.userid = this.route.snapshot.queryParamMap.get('userid');
  }

  onSubmit() {
    this.alertService.info('Init Change Password');
    this.progressBar.startLoading();
    this.authService.changePassword(this.model).subscribe(() => {
      console.log("success");
      this.alertService.success('Password Changed');
      this.changedPassword = true;
      this.progressBar.completeLoading();

    }, error => {
      console.log(error);
      this.alertService.danger('Unable to Change Password');
      this.hasError = true;
      this.progressBar.completeLoading();

    });
  }

}
