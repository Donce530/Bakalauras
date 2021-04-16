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
  public qrUrl: string;
  public images: any[];
  constructor(private _dataService: RestaurantDataService,
    private _sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this._dataService.getQrCode().subscribe(
      (qr: Blob) => {
        this.qrUrl = URL.createObjectURL(qr);
        this.qrSource = this._sanitizer.bypassSecurityTrustUrl(this.qrUrl);
        this.images = [
          { src: this.qrSource }
        ];
      }
    );
  }

  public download(): void {
    const a = document.createElement('a');
    document.body.appendChild(a);
    a.setAttribute('style', 'display: none');
    a.href = this.qrUrl;
    a.download = 'Programėlės kodas.png';
    a.click();
    window.URL.revokeObjectURL(this.qrUrl);
    a.remove();
  }
}
