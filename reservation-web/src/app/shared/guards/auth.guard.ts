import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentUser = this.authenticationService.currentUser;
    const currentTime = new Date().getTime() / 1000;
    if (currentUser) {
      var decodedToken = jwtDecode(currentUser?.token) as any;
      if (currentTime > decodedToken.exp ||
        route.data.roles && route.data.roles.indexOf(currentUser.role) === -1) {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
        return false;
      }

      return true;
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }

}
