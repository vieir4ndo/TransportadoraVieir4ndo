import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(
    public authService: AuthService,
    public progressBar: ProgressBarService,
    private alertService: AlertService,
    private userService: UserService
  ) { }

  model: any = {};

  ngOnInit(): void {
  }

  onFileChange(e: any) {
    this.model.profileimage = e.target.files[0];
  }

  onSubmit(f: NgForm) {
    this.alertService.info('Checking Information');
    this.progressBar.startLoading();
    this.progressBar.startLoading();

    debugger;
    this.model.name = f.value.name;
    this.model.federalRegistration =  f.value.federalRegistration;
    this.model.email = f.value.email;
    this.model.adress = f.value.adress;

    const updateClientObserver = {
      next: () => {
        this.alertService.success('User Updated');
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log(err);
        this.alertService.danger('Unable to Updated User');
        this.progressBar.completeLoading();
      }
    };
    this.userService.updateClient(this.model).subscribe(updateClientObserver);
  }
}

