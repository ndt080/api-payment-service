import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {NotFoundRoutingModule} from './not-found-routing.module';
import {NotFoundComponent} from './not-found.component';
import {NavBarModule} from "../../../shared/components/nav-bar/nav-bar.module";

@NgModule({
  declarations: [
    NotFoundComponent,
  ],
  imports: [
    CommonModule,
    NotFoundRoutingModule,
    NavBarModule
  ]
})
export class NotFoundModule {
}
