import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  onSubmit(f: NgForm) {
    const registerObserver = {
      next: () => console.log('User client created'),
      error: (err: any) => console.log(err)
    };

    this.authService.registerUser(f.value, "client").subscribe(registerObserver);
  }
}
