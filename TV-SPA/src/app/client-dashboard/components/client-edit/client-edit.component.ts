import { Component, OnInit } from '@angular/core';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-client-edit',
  templateUrl: './client-edit.component.html',
  styleUrls: ['./client-edit.component.scss']
})
export class ClientEditComponent implements OnInit {

  constructor(
    private authService: AuthService,
    public progressBar: ProgressBarService,
    private alertService: AlertService,
    private userService: UserService
  ) { }

  model: any = {};

  ngOnInit(): void {
  }

  onFileChange(e: any) {
    this.model = e.target.files[0];
  }

  onSubmit() {
    this.alertService.info('Checking User Info');
    this.progressBar.startLoading();
    this.progressBar.startLoading();
    const updateClientObserver = {
      next: () => {
        this.alertService.success('Account Updated');
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log(err);
        this.alertService.danger('Unable to Updated Account');
        this.progressBar.completeLoading();
      }
    };

    this.userService.updateClient(this.model).subscribe(updateClientObserver);
  }
}
