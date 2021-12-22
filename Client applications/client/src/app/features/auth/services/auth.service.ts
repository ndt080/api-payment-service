import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { tap } from "rxjs/operators";

import { environment } from "src/environments/environment";
import { User } from "@models/user.model";
import { NotificationService } from "@core/services/notification.service";
import { Router } from "@angular/router";
import { StorageAuthService } from "./storage-auth.service";
import { Tokens } from "@models/tokens.model";
import { UserStorageService } from "@features/auth/services/user-storage.service";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private http: HttpClient, private notify: NotificationService,
              private userStorageService: UserStorageService,
              private router: Router, private storage: StorageAuthService) {
  }

  login(user: User): Observable<User> {
    return this.http.post<User>(`${environment.paymentUrl}/Auth/login`, user)
      .pipe(
        tap((user) => {
            this.userStorageService.setUserData(user);
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

  register(user: User): Observable<User> {
    return this.http.post<any>(`${environment.paymentUrl}/Auth/register`, user)
      .pipe(
        tap((user) => {
            this.userStorageService.setUserData(user);
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
      this.userStorageService.removeUserData();
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
    return this.http.post<any>(`${environment.paymentUrl}/Auth/refresh-token`, {}).pipe(
      tap((user) => {
          this.userStorageService.setUserData(user);
          this.storage.storeTokens({
            access: user.jwtToken,
            refresh: user.refreshToken,
          } as Tokens);
        },
        (err) => {
          this.notify.showError(err.error.message, "Error: Refresh token");
        },
        () => this.notify.showSuccess("Refresh token complete!", "Refresh token")
      ));
  }
}
