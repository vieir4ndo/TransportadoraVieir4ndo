import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  constructor(private route: ActivatedRoute, private authService: AuthService) { }

  emailConfirmed: boolean = false;
  model: any = {};

  ngOnInit(): void {
    this.model.token = this.route.snapshot.queryParamMap.get('token');
    this.model.userid = this.route.snapshot.queryParamMap.get('userid');
  }

  onSubmit(): void {
    this.authService.confirmEmail(this.model).subscribe(() => {
      console.log("success");
      this.emailConfirmed = true;
    }, error => {
      console.log(error);
      this.emailConfirmed = false
    });
  }

}
