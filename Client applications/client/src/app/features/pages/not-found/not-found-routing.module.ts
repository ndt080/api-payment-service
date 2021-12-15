import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './not-found.component';
import {GlobalGuard} from "../../../core/guards/global.guard";

const routes: Routes = [{
  path: '',
  component: NotFoundComponent,
  canActivate: [GlobalGuard]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NotFoundRoutingModule { }
