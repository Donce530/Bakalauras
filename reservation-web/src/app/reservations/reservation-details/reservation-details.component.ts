import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ReservationDetails } from '../models/reservation-details';
import { ReservationsDataService } from '../services/reservations-data.service';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.css']
})
export class ReservationDetailsComponent implements OnInit {

  @Input() id: number;
  public details: ReservationDetails;

  constructor(private _reservationDataService: ReservationsDataService) { }

  public getFormattedDate(date: Date): string {
    const format = 'dd-MM-yyyy';
    const locale = 'en-US';
    return formatDate(date, format, locale);
  }

  ngOnInit(): void {
    this._reservationDataService.getDetails(this.id).subscribe(
      (details) => this.details = details
    );
  }
}
