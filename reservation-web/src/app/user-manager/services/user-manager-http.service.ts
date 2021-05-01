import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedResponse } from 'src/app/shared/models/data/paged-response';
import { environment } from 'src/environments/environment';
import { RegisterRequest } from '../models/register-request';
import { UserDataRow } from '../models/user-data-row';
import { UsersRequest } from '../models/users-request';

@Injectable()
export class UserManagerHttpService {
  constructor(private _httpClient: HttpClient) { }

  public getUsers(request: UsersRequest): Observable<PagedResponse<UserDataRow>> {
    const url = `${environment.apiUrl}/User/PagedAndFiltered`;

    return this._httpClient.post<PagedResponse<UserDataRow>>(url, request);
  }

  registerUser(request: RegisterRequest): Observable<void> {
    const url = `${environment.apiUrl}/User/Register`;

    return this._httpClient.post<void>(url, request);
  }

  public deleteUser(userId: number): Observable<void> {
    const url = `${environment.apiUrl}/User/${userId}`;

    return this._httpClient.delete<void>(url);
  }
}
