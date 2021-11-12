import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

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
    const registerObserver = {
      next: () => {
        console.log('User client created');
        this.alertService.success("User Registered");
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log(err);
        this.alertService.danger(err.error.errors[0].description);
        this.progressBar.completeLoading();
      }
    };

    this.authService.registerUser(f.value, "client").subscribe(registerObserver);
  }
}
