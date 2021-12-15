import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { tap } from "rxjs/operators";

import { environment } from "src/environments/environment";
import { UserModel } from "@models/user.model";
import { NotificationService } from "@core/services/notification.service";
import { Router } from "@angular/router";
import { StorageAuthService } from "./storage-auth.service";
import { Tokens } from "@models/tokens.model";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private http: HttpClient, private notify: NotificationService,
              private router: Router, private storage: StorageAuthService) {
  }

  login(user: UserModel): Observable<UserModel> {
    return this.http.post<UserModel>(`${environment.paymentUrl}/Auth/login`, user)
      .pipe(
        tap((user) => {
            this.storage.storeTokens({
              access: user.jwtToken,
              refresh: user.refreshToken,
            } as Tokens);
          },
          (err) => {
            this.notify.showError(err.error.message, "Error: Sign in");
          },
          () => this.notify.showSuccess("Sign in complete!", "Sign in"),
        ),
      );
  }

  register(user: UserModel): Observable<UserModel> {
    return this.http.post<any>(`${environment.paymentUrl}/Auth/register`, user)
      .pipe(
        tap((user) => {
            this.storage.storeTokens({
              access: user.jwtToken,
              refresh: user.refreshToken,
            } as Tokens);
          },
          err => {
            this.notify.showError(err.error.message, "Error: Sign up");
          },
          () => this.notify.showSuccess("Sign up complete!", "Sign up"),
        ),
      );
  }

  logout(): Observable<boolean> {
    try {
      this.storage.removeTokens();
      this.router.navigate(["/login"]);
    } catch (e) {
      this.notify.showError(e, "Error: Logout");
      return of(false);
    }

    this.notify.showSuccess("Logout complete!", "Logout");
    return of(true);
  }

  isLoggedIn() {
    return !!this.storage.getJwtToken();
  }

  refreshToken() {
    return this.http.post<any>(`${environment.paymentUrl}/api/Auth/revoke-token`,
      {
        token: this.storage.getRefreshToken(),
      }).pipe(
      tap((resp) => {
          this.storage.storeJwtToken(resp?.["tokens"]?.["acessToken"]);
          this.notify.showSuccess("Refresh token complete!", "Refresh token");
        },
        (err) => {
          this.notify.showError(err.error.message, "Error: Refresh token");
        },
        () => console.log("Complete refresh token"),
      ));
  }
}
