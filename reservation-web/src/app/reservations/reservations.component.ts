import { formatDate } from '@angular/common';
import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { Filters } from './models/filters';
import { PagedResponse } from './models/paged-response';
import { Paginator } from './models/paginator';
import { ReservationDataRow } from './models/reservation-data-row';
import { ReservationsRequest } from './models/reservations-request';
import { ReservationsDataService } from './services/reservations-data.service';

@Component({
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent {

  public reservations: ReservationDataRow[] = [];
  public paginator = new Paginator({
    rows: 10,
    offset: 0,
    totalRows: 0
  });
  public loading = true;

  public startAfter: Date;
  public startBefore: Date;
  public endAfter: Date;
  public endBefore: Date;
  public realStartAfter: Date;
  public realStartBefore: Date;
  public realEndAfter: Date;
  public realEndBefore: Date;

  constructor(private _reservationsDataService: ReservationsDataService) { }

  public getFormattedDate(date: Date): string {
    const format = 'dd-MM-yyyy';
    const locale = 'en-US';
    return formatDate(date, format, locale);
  }

  public loadData(event: LazyLoadEvent): void {
    this.loading = true;

    this.paginator.sortBy = event.sortField;
    this.paginator.sortOrder = event.sortOrder;
    this.paginator.rows = event.rows;
    this.paginator.offset = Math.floor(event.first / event.rows);

    const request = new ReservationsRequest({
      filters: new Filters({
        name: event.filters?.User?.value?.value,
        tableNumber: parseInt(event.filters?.TableNumber?.value?.value, 10),
        startAfter: this.startAfter != null ? new Date(Date.UTC(this.startAfter.getFullYear(),
          this.startAfter.getMonth(), this.startAfter.getDate(), this.startAfter.getHours(), this.startAfter.getMinutes())) : null,
        startUntil: this.startBefore != null ? new Date(Date.UTC(this.startBefore.getFullYear(),
          this.startBefore.getMonth(), this.startBefore.getDate(), this.startBefore.getHours(), this.startBefore.getMinutes())) : null,
        endAfter: this.endAfter != null ? new Date(Date.UTC(this.endAfter.getFullYear(),
          this.endAfter.getMonth(), this.endAfter.getDate(), this.endAfter.getHours(), this.endAfter.getMinutes())) : null,
        endUntil: this.endBefore != null ? new Date(Date.UTC(this.endBefore.getFullYear(),
          this.endBefore.getMonth(), this.endBefore.getDate(), this.endBefore.getHours(), this.endBefore.getMinutes())) : null,
        realStartAfter: this.realStartAfter != null ? new Date(Date.UTC(this.realStartAfter.getFullYear(),
          this.realStartAfter.getMonth(), this.realStartAfter.getDate(), this.realStartAfter.getHours(), this.realStartAfter.getMinutes())) : null,
        realStartUntil: this.realEndBefore != null ? new Date(Date.UTC(this.realEndBefore.getFullYear(),
          this.realEndBefore.getMonth(), this.realEndBefore.getDate(), this.realEndBefore.getHours(), this.realEndBefore.getMinutes())) : null,
        realEndAfter: this.realEndAfter != null ? new Date(Date.UTC(this.realEndAfter.getFullYear(),
          this.realEndAfter.getMonth(), this.realEndAfter.getDate(), this.realEndAfter.getHours(), this.realEndAfter.getMinutes())) : null,
        realEndUntil: this.realEndBefore != null ? new Date(Date.UTC(this.realEndBefore.getFullYear(),
          this.realEndBefore.getMonth(), this.realEndBefore.getDate(), this.realEndBefore.getHours(), this.realEndBefore.getMinutes())) : null,
        day: event.filters?.Day?.value != null ? new Date(Date.UTC(event.filters?.Day.value.getFullYear(),
          event.filters?.Day?.value.getMonth(), event.filters?.Day?.value.getDate())) : null,
      }),
      paginator: this.paginator
    });

    this._reservationsDataService.getReservations(request).subscribe(
      (data: PagedResponse<ReservationDataRow>) => {
        this.reservations = data.results;
        this.paginator = data.paginator;
        this.loading = false;
      }
    );
  }

}
