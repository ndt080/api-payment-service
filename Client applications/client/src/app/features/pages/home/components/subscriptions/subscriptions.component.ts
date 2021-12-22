import { Component, OnDestroy } from "@angular/core";
import { UserStorageService } from "@features/auth/services/user-storage.service";
import { Subscription } from "@models/subscription.model";
import { ApiService } from "@services/api.service";
import { of, Subject } from "rxjs";
import { switchMap, takeUntil } from "rxjs/operators";
import { User } from "@models/user.model";

@Component({
  selector: "Subscriptions",
  templateUrl: "./subscriptions.component.html",
  styleUrls: ["./subscriptions.component.scss"],
})
export class SubscriptionsComponent implements OnDestroy {
  subscriptions: Subscription[] = [];
  private destroy = new Subject<boolean>();

  constructor(private storage: UserStorageService, private apiService: ApiService) {
    this.getSubscriptions();
  }

  unsubscribe(apiKey: string) {
    this.apiService.unSubscribeService(apiKey).pipe(takeUntil(this.destroy)).subscribe(() => {
      this.getSubscriptions();
    });
  }

  getSubscriptions() {
    this.apiService.getUserData().pipe(
      takeUntil(this.destroy),
      switchMap(result => of(result))
    ).subscribe((user: User) => {
      this.subscriptions = user.subscriptions as Subscription[];
    });
  }

  ngOnDestroy() {
    this.destroy.next(true);
  }
}
