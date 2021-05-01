import { Component, OnDestroy, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MenuItem, PrimeNGConfig } from 'primeng/api';
import { AuthenticationService } from './shared/services/authentication.service';
import { Role } from './shared/enums/role.enum';
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { SubscriptionLike } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  public menuItems: MenuItem[];
  public activeItem: MenuItem;
  public icons = {
    signOut: faSignOutAlt
  };

  private _subscription: SubscriptionLike;

  constructor(public authService: AuthenticationService,
    private _location: Location,
    private _router: Router,
    private _config: PrimeNGConfig) { }

  ngOnInit(): void {
    this._subscription = this.authService.currentUserObservable.subscribe(() => this._initializeMenuItems());

    this._initializeMenuItems();
    this._config.setTranslation({
      dayNames: ['Sekmadienis', 'Pirmadienis', 'Antradienis', 'Trečiadienis', 'Ketvirtadienis',
        'Penktadienis', 'Šeštadienis', 'Sekmadienis'],
      dayNamesShort: ['Se', 'Pi', 'An', 'Tr', 'Ke', 'Pe', 'Še'],
      dayNamesMin: ['Se', 'Pi', 'An', 'Tr', 'Ke', 'Pe', 'Še'],
      today: 'Šiandien',
      clear: 'Išvalyti'
    });
  }

  ngOnDestroy(): void {
    if (this._subscription) {
      this._subscription.unsubscribe();
    }
  }

  private _initializeMenuItems(): void {
    if (this.authService.currentUser?.role === Role.Admin) {
      this.menuItems = [
        { label: 'Vartotojai', icon: 'fa fa-utensils', routerLink: 'users' },
      ];
      this.activeItem = this.menuItems[0];
    } else {
      this.menuItems = [
        { label: 'Restoranas', icon: 'fa fa-utensils', routerLink: 'restaurant' },
        { label: 'Rezervacijos', icon: 'fa fa-concierge-bell', routerLink: 'reservations' }
      ];
      this.activeItem = this._location.path() === '/restaurant' ? this.menuItems[0] : this.menuItems[1];
    }
  }

  public exit(): void {
    this.authService.logout();
    this._router.navigateByUrl('login');
  }
}
