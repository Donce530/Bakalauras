import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { LoginRequest } from 'src/app/login/models/login-request';


@Injectable()
export class AuthenticationService {
  public currentUserObservable: Observable<User>;

  private _currentUserSubject: BehaviorSubject<User>;

  constructor(private http: HttpClient) {
    const currentUser = localStorage.getItem('currentUser');
    this._currentUserSubject = new BehaviorSubject<User>(JSON.parse(currentUser));
    this.currentUserObservable = this._currentUserSubject.asObservable();
  }

  public get currentUser(): User {
    return this._currentUserSubject.value;
  }

  login(request: LoginRequest): Observable<User> {
    return this.http.post<User>(`${environment.apiUrl}/token/authenticate`, request).pipe(
      map(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this._currentUserSubject.next(user);
        }
        return user;
      })
    );
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this._currentUserSubject.next(null);
  }
}
