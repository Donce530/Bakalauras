import { Component, OnInit } from '@angular/core';
import { FaIconComponent } from '@fortawesome/angular-fontawesome';
import { ConfirmationService, LazyLoadEvent, MessageService } from 'primeng/api';
import { Role } from '../shared/enums/role.enum';
import { PagedResponse } from '../shared/models/data/paged-response';
import { Paginator } from '../shared/models/data/paginator';
import { UserDataRow } from './models/user-data-row';
import { UserFilters } from './models/user-filters';
import { UsersRequest } from './models/users-request';
import { UserManagerDataService } from './services/user-manager-data.service';

@Component({
  selector: 'app-user-manager',
  templateUrl: './user-manager.component.html',
  styleUrls: ['./user-manager.component.css']
})
export class UserManagerComponent {
  public roleFilterOptions = [
    { label: 'Visos', value: null },
    { label: 'Vadybininkas', value: Role.Manager },
    { label: 'Klientas', value: Role.User }
  ];
  public currentRoleOption = this.roleFilterOptions[0];

  public users: UserDataRow[] = [];
  public paginator = new Paginator({
    rows: 10,
    offset: 0,
    totalRows: 0
  });
  public visible = true;
  public loading = true;
  public showNewManagerDialog = false;
  private _lastLazyLoadEvent: LazyLoadEvent;

  constructor(private _userManagerDataService: UserManagerDataService,
    private _confirmationService: ConfirmationService,
    private _messageService: MessageService) { }

  public loadData(event: LazyLoadEvent): void {
    this._lastLazyLoadEvent = event;
    this.loading = true;

    this.paginator.sortBy = event.sortField;
    this.paginator.sortOrder = event.sortOrder;
    this.paginator.rows = event.rows;
    this.paginator.offset = Math.floor(event.first / event.rows);

    const request = new UsersRequest({
      filters: new UserFilters({
        firstName: event.filters?.FirstName?.value?.value,
        lastName: event.filters?.LastName?.value?.value,
        email: event.filters?.Email?.value?.value,
        phoneNumber: event.filters?.PhoneNumber?.value?.value,
        role: this.currentRoleOption.value,
      }),
      paginator: this.paginator
    });

    this._userManagerDataService.getUsers(request).subscribe(
      (data: PagedResponse<UserDataRow>) => {
        this.users = data.results;
        this.paginator = data.paginator;
        this.loading = false;
      }
    );
  }

  public getRoleLabel(role: Role): string {
    return role === Role.Manager ? 'Vadybininkas' : 'Klientas';
  }

  public newManager(): void {
    this.showNewManagerDialog = true;
  }

  public deleteUser(user: UserDataRow): void {
    this._confirmationService.confirm({
      message: `Ar tikrai norite ištrinti vartotoją ${user.firstName} ${user.lastName}?`,
      accept: () => {
        this._userManagerDataService.deleteUser(user.id).subscribe(() => {
          this._messageService.add({ severity: 'success', summary: 'Pavyko!', detail: 'Vartotojas ištrintas' });
          this._refreshTable();
        });
      }
    });
  }

  public onManagerDialogClose(refresh: boolean): void {
    this.showNewManagerDialog = false;
    if (refresh) {
      this._refreshTable();
    }
  }

  private _refreshTable(): void {
    this.loadData(this._lastLazyLoadEvent);
  }
}
