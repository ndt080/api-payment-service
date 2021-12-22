import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { MailingRoutingModule } from "./mailing-routing.module";
import { MailingComponent } from "./mailing.component";
import { NavBarModule } from "@shared/components/nav-bar/nav-bar.module";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatDialogModule } from "@angular/material/dialog";

@NgModule({
  declarations: [MailingComponent],
  imports: [
    CommonModule,
    MailingRoutingModule,
    NavBarModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
  ],
})
export class MailingModule {
}
