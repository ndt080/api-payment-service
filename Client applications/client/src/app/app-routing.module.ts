import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
  { path: "login", loadChildren: () => import("./features/auth/pages/login/login.module").then(m => m.LoginModule) },
  { path: "mailing", loadChildren: () => import("./features/pages/mailing/mailing.module").then(m => m.MailingModule) },
  { path: "", loadChildren: () => import("./features/pages/home/home.module").then(m => m.HomeModule) },
  { path: "**", loadChildren: () => import("./features/pages/not-found/not-found.module").then(m => m.NotFoundModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
}

