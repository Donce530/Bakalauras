import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { MenuItem, PrimeNGConfig } from 'primeng/api';
import { AuthenticationService } from './shared/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public menuItems: MenuItem[] = [
    { label: 'Restoranas', icon: 'fa fa-utensils', command: () => { this._router.navigateByUrl('restaurant'); } },
    { label: 'Rezervacijos', icon: 'fa fa-concierge-bell', command: () => { this._router.navigateByUrl('reservations'); } }
  ];
  public activeItem: MenuItem;

  constructor(public authService: AuthenticationService,
    private _router: Router,
    private _location: Location,
    private _config: PrimeNGConfig) { }

  ngOnInit(): void {
    this.activeItem = this._location.path() === '/restaurant' ? this.menuItems[0] : this.menuItems[1];

    this._config.setTranslation({
      dayNames: ['Sekmadienis', 'Pirmadienis', 'Antradienis', 'Trečiadienis', 'Ketvirtadienis',
        'Penktadienis', 'Šeštadienis', 'Sekmadienis'],
      dayNamesShort: ['Se', 'Pi', 'An', 'Tr', 'Ke', 'Pe', 'Še'],
      dayNamesMin: ['Se', 'Pi', 'An', 'Tr', 'Ke', 'Pe', 'Še'],
      today: 'Šiandien',
      clear: 'Išvalyti'
    });
  }
}
