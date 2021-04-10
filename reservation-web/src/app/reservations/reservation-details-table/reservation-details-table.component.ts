import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ReservationDetails } from '../models/reservation-details';

@Component({
  selector: 'app-reservation-details-table',
  templateUrl: './reservation-details-table.component.html',
  styleUrls: ['./reservation-details-table.component.css']
})
export class ReservationDetailsTableComponent implements OnInit {

  @Input() details: ReservationDetails[];

  constructor() { }

  ngOnInit(): void {
  }

  public getFormattedDate(date: Date): string {
    const format = 'dd-MM-yyyy';
    const locale = 'en-US';
    return formatDate(date, format, locale);
  }

}
