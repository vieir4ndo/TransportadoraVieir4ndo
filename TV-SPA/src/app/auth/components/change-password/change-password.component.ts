import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  constructor(private route: ActivatedRoute, private authService: AuthService, public progressBar: ProgressBarService) { }

  model: any = {};
  changedPassword: boolean = false;
  hasError: boolean = false;

  ngOnInit(): void {
    this.model.token = this.route.snapshot.queryParamMap.get('token');
    this.model.userid = this.route.snapshot.queryParamMap.get('userid');
  }

  onSubmit() {
    this.progressBar.startLoading();
    this.authService.changePassword(this.model).subscribe(() => {
      console.log("success");
      this.changedPassword = true;
      this.progressBar.completeLoading();

    }, error => {
      console.log(error);
      this.hasError = true;
      this.progressBar.completeLoading();

    });
  }

}
