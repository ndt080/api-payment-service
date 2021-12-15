import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home.component';
import {GlobalGuard} from "../../../core/guards/global.guard";

const routes: Routes = [{
  path: '',
  canActivate: [GlobalGuard],
  component: HomeComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
