import { Component, OnInit } from '@angular/core';
import { LoginFormService } from '../_formServices/loginForm.service';
import { AuthenticationService } from '../_services/authentication.service';
import { first, finalize } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  get form() { return this.formService.f };
  get f() { return this.form.controls };
  get isLoggedIn() { return this.authService.isLoggedIn };

  constructor(
    private formService: LoginFormService,
    private authService: AuthenticationService,
    private spinner: NgxSpinnerService
    ) { 

  }

  ngOnInit() {
  }

  logout() {
    this.authService.logout();
  }

  submit() {
    if (!this.form.valid) {
      alert('Form not valid');
      return;
    }
    this.spinner.show();
    this.authService.login(this.f.username.value, this.f.password.value)
    .pipe(finalize(() => {this.spinner.hide()}))
    .pipe(first())
    .subscribe(res => {
      console.log(res);
    }, err => {
      console.log(err);
    });
  }

}
