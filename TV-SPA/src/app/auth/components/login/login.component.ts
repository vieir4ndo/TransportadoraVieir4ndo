import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, public progressBar: ProgressBarService) { }

  ngOnInit(): void {

  }

  onSubmit(f: NgForm) {
    this.progressBar.startLoading();
    this.progressBar.startLoading();
    const loginObserver = {
      next: () => {
        console.log('User logged in');
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log(err);
        this.progressBar.completeLoading();
      }
    };

    this.authService.login(f.value).subscribe(loginObserver);
  }
}
