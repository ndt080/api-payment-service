import { Component, OnDestroy } from "@angular/core";
import { UserStorageService } from "@features/auth/services/user-storage.service";
import { ApiService } from "@services/api.service";
import { of, Subject } from "rxjs";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { takeUntil } from "rxjs/operators";

@Component({
  selector: "SubscribeModalView",
  templateUrl: "./subscribe-modal.component.html",
  styleUrls: ["./subscribe-modal.component.scss"],
})
export class SubscribeModalComponent implements OnDestroy {
  private destroy = new Subject<boolean>();

  form = new FormGroup({
    serviceName: new FormControl("", [
      Validators.required,
      Validators.minLength(3)
    ]),
    paymentAmount: new FormControl(0, [
      Validators.required,
      Validators.min(0)
    ]),
  });

  constructor(private apiService: ApiService) {}

  onSubmit() {
    this.apiService.subscribeService(this.form.value).pipe(takeUntil(this.destroy)).subscribe(result => {
      if(result) {
        this.apiService.getUserData().pipe(takeUntil(this.destroy)).subscribe()
      }
    })
  }

  ngOnDestroy() {
    this.destroy.next(true);
  }
}
