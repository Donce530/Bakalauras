import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faUser } from '@fortawesome/free-regular-svg-icons';
import { faKey } from '@fortawesome/free-solid-svg-icons';
import { MessageService } from 'primeng/api';
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
    private _router: Router,
    private _messageService: MessageService) { }

  public ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
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
      },
      error => this._messageService.add({ severity: 'error', summary: 'Klaida!', detail: 'Neteisingi prisijungimo duomenys' })
    );
  }

}
