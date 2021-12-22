import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AuthGuard} from "../../core/guards/auth.guard";
import {AuthService} from "./services/auth.service";
import {GlobalGuard} from "../../core/guards/global.guard";
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {TokenInterceptor} from "./token.interceptor";
import {StorageAuthService} from "./services/storage-auth.service";

@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ],
    providers: [
        AuthGuard,
        AuthService,
        StorageAuthService,
        GlobalGuard,
    ],
})
export class AuthModule {
}
