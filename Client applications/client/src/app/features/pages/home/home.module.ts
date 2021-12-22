import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { HomeRoutingModule } from "./home-routing.module";
import { NavBarModule } from "@shared/components/nav-bar/nav-bar.module";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatDialogModule } from "@angular/material/dialog";
import { MailingComponent } from "@features/pages/mailing/mailing.component";
import { HomeComponent } from "@features/pages/home/home.component";
import { SubscriptionsComponent } from "@features/pages/home/components/subscriptions/subscriptions.component";
import { SubscribeModalComponent } from "@features/pages/home/components/subscribe-modal/subscribe-modal.component";

@NgModule({
  declarations: [HomeComponent, SubscriptionsComponent, SubscribeModalComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
    NavBarModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
  ],
})
export class HomeModule {
}
