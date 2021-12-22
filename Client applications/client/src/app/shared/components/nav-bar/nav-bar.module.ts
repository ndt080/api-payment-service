import {NgModule} from '@angular/core';
import {NavBarComponent} from "./nav-bar.component";
import {RouterModule} from "@angular/router";
import {CommonModule} from "@angular/common";

@NgModule({
  declarations: [
    NavBarComponent,
  ],
  imports: [
    RouterModule,
    CommonModule
  ],
  exports: [
    NavBarComponent,
  ]
})
export class NavBarModule {
}
