import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MailingComponent } from "./mailing.component";
import { GlobalGuard } from "@core/guards/global.guard";

const routes: Routes = [{
  path: "",
  canActivate: [GlobalGuard],
  component: MailingComponent,
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MailingRoutingModule {
}
