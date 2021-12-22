import { Injectable } from "@angular/core";
import { User } from "@models/user.model";
import { environment } from "../../../environments/environment";
import { tap } from "rxjs/operators";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { UserStorageService } from "@features/auth/services/user-storage.service";
import { NotificationService } from "@services/notification.service";

@Injectable({
  providedIn: "root",
})
export class ApiService {
  constructor(private http: HttpClient, private userStorageService: UserStorageService,
              private notify: NotificationService) {
  }

  unSubscribeService(apiKey: string): Observable<User> {
    return this.http.delete<any>(`${environment.paymentUrl}/Subscribe/unsubscribe?apiKey=${apiKey}`)
      .pipe(
        tap(() => {
          },
          (err) => {
            console.log(err);
            this.notify.showError(err.error.message, "Error unsubscribe!");
          },
          () => this.notify.showSuccess("Success unsubscribe!", ""),
        ),
      );
  }

  subscribeService(data: any): Observable<User> {
    return this.http.post<any>(`${environment.paymentUrl}/Subscribe/subscribe`, data)
      .pipe(
        tap(() => {
          },
          (err) => {
            this.notify.showError(err.error.message, "Error subscribe!");
          },
          () => this.notify.showSuccess("Success subscribe!", ""),
        ),
      );
  }

  sendEmail(data: any, apiKey: string): Observable<User> {
    return this.http.post<any>(`${environment.mailing}/Mailing/SendEmail?accessKey=${apiKey}`, data)
      .pipe(
        tap(() => {},
          (err) => {
            this.notify.showError(err.error.message, "Error send email!");
          },
          () => this.notify.showSuccess("Success send email!", ""),
        ),
      );
  }


  getUserData(): Observable<User> {
    return this.http.get<User>(`${environment.paymentUrl}/Auth/user`)
      .pipe(
        tap((user) => {
            this.userStorageService.setUserData(user);
          },
          (err) => {
            console.log(err);
            this.notify.showError(err.error.message, "Get user data error!");
          },
        ),
      );
  }
}
