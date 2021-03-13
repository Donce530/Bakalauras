import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class RestaurantBehaviourService {

  private _editorOpenState = new BehaviorSubject<boolean>(false);

  public get editorOpenStateObservable(): Observable<boolean> {
    return this._editorOpenState.asObservable();
  }

  public setEditorOpenState(value: boolean): void {
    this._editorOpenState.next(value);
  }
}
