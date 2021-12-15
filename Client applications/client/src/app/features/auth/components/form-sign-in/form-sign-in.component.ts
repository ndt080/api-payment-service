import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthService} from "../../services/auth.service";
import {HttpClient} from "@angular/common/http";
import { Router } from "@angular/router";

@Component({
  selector: 'form-sign-in',
  templateUrl: './form-sign-in.component.html',
  styleUrls: ['../../assets/styles/form-sign.scss']
})
export class FormSignInComponent {
  form: FormGroup = new FormGroup({
    "email": new FormControl("", [
      Validators.required,
      Validators.email
    ]),
    "password": new FormControl("", [
      Validators.required,
      Validators.minLength(6)
    ])
  });

  constructor(private http: HttpClient, private auth: AuthService, private router: Router) {
  }

  onSubmit() {
    this.auth.login(this.form.value).subscribe(user => {
      user && this.router.navigate(["/"]);
    });
  }
}
