import {Injectable} from '@angular/core';
import {Tokens} from "@models/tokens.model";

const JWT_TOKEN = 'JWT_TOKEN'
const REFRESH_TOKEN = 'REFRESH_TOKEN'

@Injectable({
  providedIn: 'root'
})
export class StorageAuthService {

  getJwtToken() {
    return localStorage.getItem(JWT_TOKEN);
  }

  getRefreshToken() {
    return localStorage.getItem(REFRESH_TOKEN);
  }

  storeJwtToken(jwt: string) {
    localStorage.setItem(JWT_TOKEN, jwt);
  }

  storeTokens(tokens: Tokens) {
    localStorage.setItem(JWT_TOKEN, tokens.access);
    localStorage.setItem(REFRESH_TOKEN, tokens.refresh);
  }

  removeTokens() {
    localStorage.removeItem(JWT_TOKEN);
    localStorage.removeItem(REFRESH_TOKEN);
  }
}
