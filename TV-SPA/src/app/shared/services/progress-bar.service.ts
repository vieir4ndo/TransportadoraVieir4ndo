import { Injectable } from '@angular/core';
import { NgProgressRef } from '@ngx-progressbar/core';

@Injectable({
  providedIn: 'root'
})
export class ProgressBarService {
  constructor() { }

  progressRef!: NgProgressRef;

  startLoading() {
    this.progressRef.start();
  }

  completeLoading() {
    this.progressRef.complete();
  }

  incLoading(n: number) {
    this.progressRef.inc(n);
  }

  setLoading(n: number) {
    this.progressRef.set(n);
  }
}
