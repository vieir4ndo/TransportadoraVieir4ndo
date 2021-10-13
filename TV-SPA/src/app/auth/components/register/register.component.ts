import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private authService: AuthService, public progressBar: ProgressBarService) { }

  ngOnInit(): void {
  }

  onSubmit(f: NgForm) {
    this.progressBar.startLoading();
    const registerObserver = {
      next: () => {
        console.log('User client created');
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log(err);
        this.progressBar.completeLoading();
      }
    };

    this.authService.registerUser(f.value, "client").subscribe(registerObserver);
  }
}
