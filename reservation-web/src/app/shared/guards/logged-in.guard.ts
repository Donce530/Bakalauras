import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class LoggedInGuard implements CanActivate {

  constructor(private _authenticationService: AuthenticationService,
    private _router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this._authenticationService.currentUser) {

      return true;
    }

    return false;
  }
}
