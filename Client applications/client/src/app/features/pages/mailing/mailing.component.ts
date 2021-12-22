import { Component, OnDestroy } from "@angular/core";
import { Subscription } from "@models/subscription.model";
import { of, Subject } from "rxjs";
import { UserStorageService } from "@features/auth/services/user-storage.service";
import { ApiService } from "@services/api.service";
import { switchMap, takeUntil } from "rxjs/operators";
import { User } from "@models/user.model";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "mailing-page",
  templateUrl: "./mailing.component.html",
  styleUrls: ["./mailing.component.scss"],
})
export class MailingComponent implements OnDestroy {
  private destroy = new Subject<boolean>();
  subscriptions: Subscription[] = [];
  mailingSubscription: Subscription | any;

  form = new FormGroup({
    toEmail: new FormControl("", [
      Validators.required,
      Validators.email,
    ]),
    subject: new FormControl("", [
      Validators.required,
      Validators.minLength(3)
    ]),
    body: new FormControl("", [
      Validators.required,
      Validators.minLength(3)
    ]),
  });

  constructor(private storage: UserStorageService, private apiService: ApiService) {
    this.subscriptions = this.storage.getUserData().subscriptions as Subscription[];
    this.mailingSubscription = this.subscriptions.find(subs => subs.serviceName == "MailingService");
    console.log(this.subscriptions)
    console.log(this.mailingSubscription)
  }

  onSubmit() {
    this.apiService.sendEmail(this.form.value, this.mailingSubscription.apiKey).pipe(
      takeUntil(this.destroy),
      switchMap(result => of(result))
    ).subscribe(() => {
      this.mailingSubscription = this.subscriptions.find(subs => subs.serviceName == "MailingService");
    });
  }

  ngOnDestroy() {
    this.destroy.next(true);
  }
}
