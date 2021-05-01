import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedResponse } from 'src/app/shared/models/data/paged-response';
import { RegisterRequest } from '../models/register-request';
import { UserDataRow } from '../models/user-data-row';
import { UsersRequest } from '../models/users-request';
import { UserManagerHttpService } from './user-manager-http.service';

@Injectable()
export class UserManagerDataService {
  constructor(private _httpService: UserManagerHttpService) { }

  public getUsers(request: UsersRequest): Observable<PagedResponse<UserDataRow>> {
    return this._httpService.getUsers(request);
  }

  public deleteUser(userId: number): Observable<void> {
    return this._httpService.deleteUser(userId);
  }

  public registerUser(registerRequest: RegisterRequest): Observable<void> {
    return this._httpService.registerUser(registerRequest);
  }
}
