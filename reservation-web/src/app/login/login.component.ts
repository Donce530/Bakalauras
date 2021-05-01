import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faUser } from '@fortawesome/free-regular-svg-icons';
import { faKey } from '@fortawesome/free-solid-svg-icons';
import { Role } from '../shared/enums/role.enum';
import { AuthenticationService } from '../shared/services/authentication.service';
import { LoginRequest } from './models/login-request';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public icons = {
    user: faUser,
    key: faKey
  };
  public loginForm: FormGroup;

  constructor(private _authenticationService: AuthenticationService,
    private _route: ActivatedRoute,
    private _router: Router) { }

  public ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required])
    });

    this.loginForm.updateValueAndValidity();
  }

  public login(): void {
    const loginRequest: LoginRequest = this.loginForm.value;
    this._authenticationService.login(loginRequest).subscribe(
      user => {
        if (user) {
          this._router.navigateByUrl(user.role === Role.Admin ? 'users' : 'restaurants');
        }
      }
    );
  }

}
