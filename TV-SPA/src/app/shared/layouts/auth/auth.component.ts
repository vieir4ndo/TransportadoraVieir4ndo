import { Component, OnInit } from '@angular/core';
import { NgProgress } from '@ngx-progressbar/core';
import { AlertService } from 'ngx-alerts';
import { ProgressBarService } from '../../services/progress-bar.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {

  constructor(
    private progress: NgProgress,
    public progressBar: ProgressBarService,
    private alertService: AlertService,
  ) { }

  ngOnInit(): void {
    this.progressBar.progressRef = this.progress.ref("progressBar")
  }

}
