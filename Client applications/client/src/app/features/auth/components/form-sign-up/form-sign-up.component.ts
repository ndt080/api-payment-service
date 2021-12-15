import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import { Router } from "@angular/router";

@Component({
  selector: 'form-sign-up',
  templateUrl: './form-sign-up.component.html',
  styleUrls: ['../../assets/styles/form-sign.scss']
})
export class FormSignUpComponent {
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

  constructor(private auth: AuthService, private router: Router) {}

  onSubmit() {
    this.auth.register(this.form.value).subscribe(user => {
      user && this.router.navigate(["/"]);
    });
  }
}
