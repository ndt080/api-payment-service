import {Injectable} from '@angular/core';
import {Tokens} from "@models/tokens.model";
import { User } from "@models/user.model";

const USER_DATA = "USER_DATA";

@Injectable({
  providedIn: 'root'
})
export class UserStorageService {

  getUserData(): User {
    return JSON.parse(localStorage.getItem(USER_DATA) || "");
  }

  setUserData(data: User) {
    localStorage.setItem(USER_DATA, JSON.stringify(data));
  }

  removeUserData() {
    localStorage.removeItem(USER_DATA);
  }
}
