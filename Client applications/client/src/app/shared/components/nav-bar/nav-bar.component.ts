import { Component, OnDestroy } from "@angular/core";
import {Route} from "@models/route.model";
import {AuthService} from "@features/auth/services/auth.service";
import { Router } from "@angular/router";
import { takeUntil } from "rxjs/operators";
import { Subject } from "rxjs";

@Component({
  selector: 'app-nav-bar',
  template: `
    <div class="nav">
      <ul class="nav__list">
        <li class="nav__item" *ngFor="let item of navItems"><a routerLink="{{item.link}}">{{item.header}}</a></li>
        <li class="nav__item"><a href="#" (click)="logout()">Exit app</a></li>
      </ul>
    </div>
  `,
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnDestroy {
  private destroy = new Subject<boolean>();
  readonly navItems: Array<Route> = [
    {header: 'Home', link: '/'},
    {header: 'Mailing', link: '/mailing'},
  ];

  constructor(private auth: AuthService, private router: Router) { }

  ngOnDestroy() {
    this.destroy.next(true);
  }

  logout(){
    this.auth.logout().pipe(takeUntil(this.destroy)).subscribe(result => {
      result && this.router.navigate(["/login"])
    });
  }
}
