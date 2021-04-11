import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { RestaurantDataService } from '../../services/restaurant-data.service';

@Component({
  selector: 'app-restaurant-qr',
  templateUrl: './restaurant-qr.component.html',
  styleUrls: ['./restaurant-qr.component.css']
})
export class RestaurantQrComponent implements OnInit {

  public qrSource: SafeUrl;
  constructor(private _dataService: RestaurantDataService,
    private _sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this._dataService.getQrCode().subscribe(
      (qr: Blob) => {
        const url = URL.createObjectURL(qr);
        this.qrSource = this._sanitizer.bypassSecurityTrustUrl(url);
      }
    );
  }

}
