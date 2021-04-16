import { Component, OnInit } from '@angular/core';
import { faSave, faWindowClose } from '@fortawesome/free-regular-svg-icons';
import { faChair, faDoorOpen, faFont, faProjectDiagram, faTrash, faVectorSquare } from '@fortawesome/free-solid-svg-icons';
import { RestaurantBehaviourService } from 'src/app/restaurant/services/restaurant-behaviour.service';
import { EditorMode } from '../enums/editor-mode.enum';
import { EditorBehaviourService } from '../services/editor-behaviour.service';
import { ModeButton } from './models/mode-button';
import { SnappingSettings } from './models/snapping-settings';

@Component({
  selector: 'app-editor-toolbar',
  templateUrl: './editor-toolbar.component.html',
  styleUrls: ['./editor-toolbar.component.css']
})
export class EditorToolbarComponent implements OnInit {

  constructor(private _editorBehaviourService: EditorBehaviourService,
    private _restaurantBehaviourService: RestaurantBehaviourService) { }

  public modes: ModeButton[];
  public icons = {
    cancel: faWindowClose,
    save: faSave
  };

  public get currentMode(): EditorMode {
    return this._currentMode;
  }

  public set currentMode(mode: EditorMode) {
    if (mode !== this.currentMode) {
      this._editorBehaviourService.setEditorMode(mode);
      this._currentMode = mode;
    }
  }

  public get snappingSettings(): SnappingSettings {
    return this._snappingSettings;
  }

  private _currentMode: EditorMode;
  private _snappingSettings = new SnappingSettings({
    available: false,
    enabled: false,
    scale: 5
  });

  public ngOnInit(): void {
    this.modes = [
      new ModeButton({ mode: EditorMode.Wall, tooltip: 'Siena', icon: faVectorSquare }),
      new ModeButton({ mode: EditorMode.Table, tooltip: 'Stalas', icon: faChair }),
      new ModeButton({ mode: EditorMode.Link, tooltip: 'Susiejimas', icon: faProjectDiagram }),
      new ModeButton({ mode: EditorMode.Comment, tooltip: 'Komentaras', icon: faFont }),
      new ModeButton({ mode: EditorMode.Delete, tooltip: 'Naikinti', icon: faTrash }),
    ];

    const storedSnappingState = localStorage.getItem('snappingState');
    this._snappingSettings.enabled = storedSnappingState != null ? storedSnappingState === 'true' : false;
    this._editorBehaviourService.setSnappingState(this._snappingSettings.enabled);

    this._editorBehaviourService.snappingScale.subscribe(scale => this._snappingSettings.scale = scale);
    this._editorBehaviourService.snappingAvailability.subscribe(available => this._snappingSettings.available = available);

  }

  public updateSnappingScale(scale: number): void {
    if (this._snappingSettings.enabled) {
      this._snappingSettings.scale = scale;
      this._editorBehaviourService.setSnappingScale(scale);
    }
  }

  public updateSnappingState(enabled: boolean): void {
    if (this._snappingSettings.enabled !== enabled) {
      this._snappingSettings.enabled = enabled;
      this._editorBehaviourService.setSnappingState(enabled);
      localStorage.setItem('snappingState', enabled.toString());
    }
  }

  public save(): void {
    this._editorBehaviourService.initSave();
  }

  public back(): void {
    this._restaurantBehaviourService.setEditorOpenState(false);
  }
}
