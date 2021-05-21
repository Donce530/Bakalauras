import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { EditorMode } from '../enums/editor-mode.enum';
import { EditTableParameters } from '../models/edit-table-parameters';
import { InputTextParameters } from '../models/input-text-parameters';

@Injectable()
export class EditorBehaviourService implements OnDestroy {
  public get editorMode(): Observable<EditorMode> {
    return this._editorModeSubject.asObservable();
  }

  public get snappingScale(): Observable<number> {
    return this._snappingScaleSubject.asObservable();
  }

  public get snappingAvailability(): Observable<boolean> {
    return this._snappingAvailabilitySubject.asObservable();
  }

  public get snappingState(): Observable<boolean> {
    return this._snappingStateSubject.asObservable();
  }

  public get saveAction(): Observable<void> {
    return this._saveActionSubject.asObservable();
  }

  public get editTableAction(): Observable<EditTableParameters> {
    return this._editTableSubject.asObservable();
  }

  public get openTextInputAction(): Observable<InputTextParameters> {
    return this._openTextInputSubject.asObservable();
  }

  public get closeTextInputAction(): Observable<string> {
    return this._closeTextInputSubject.asObservable();
  }

  public get updatePlanAction(): Observable<void> {
    return this._planUpdateSubject.asObservable();
  }

  private _editorModeSubject = new Subject<EditorMode>();
  private _snappingScaleSubject = new Subject<number>();
  private _snappingAvailabilitySubject = new Subject<boolean>();
  private _snappingStateSubject = new BehaviorSubject<boolean>(false);
  private _saveActionSubject = new Subject<void>();
  private _editTableSubject = new Subject<EditTableParameters>();
  private _openTextInputSubject = new Subject<InputTextParameters>();
  private _closeTextInputSubject = new Subject<string>();
  private _planUpdateSubject = new Subject<void>();

  public openTextInput(parameters: InputTextParameters): void {
    this._openTextInputSubject.next(parameters);
  }

  public updatePlan(): void {
    this._planUpdateSubject.next();
  }

  public closeTextInput(text: string): void {
    this._closeTextInputSubject.next(text);
  }

  public setEditorMode(mode: EditorMode): void {
    this._editorModeSubject.next(mode);
  }

  public setSnappingScale(scale: number): void {
    this._snappingScaleSubject.next(scale);
  }

  public setSnappingAvailability(available: boolean): void {
    this._snappingAvailabilitySubject.next(available);
  }

  public setSnappingState(state: boolean): void {
    this._snappingStateSubject.next(state);
  }

  public initSave(): void {
    this._saveActionSubject.next();
  }

  public initTableEdit(parameters: EditTableParameters): void {
    this._editTableSubject.next(parameters);
  }

  constructor() { }

  ngOnDestroy(): void {
    this._editorModeSubject.complete();
    this._snappingScaleSubject.complete();
    this._snappingAvailabilitySubject.complete();
    this._snappingStateSubject.complete();
    this._saveActionSubject.complete();
    this._editTableSubject.complete();
  }
}
