import {Component} from '@angular/core';

@Component({
  selector: 'app-not-found',
  template: `
    <app-nav-bar></app-nav-bar>
    <div class="box">
      <h1 class="box__message">This page not found!!!</h1>
    </div>`,
  styleUrls: ['not-found.component.css']
})
export class NotFoundComponent {
}
