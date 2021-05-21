import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { OverlayPanel } from 'primeng/overlaypanel';
import { Table } from '../drawables/table';
import { EditTableParameters } from '../models/edit-table-parameters';
import { EditorBehaviourService } from '../services/editor-behaviour.service';
import { EditorDataService } from '../services/editor-data.service';

@Component({
  selector: 'app-editor-table-overlay',
  templateUrl: './editor-table-overlay.component.html',
  styleUrls: ['./editor-table-overlay.component.css']
})
export class EditorTableOverlayComponent implements OnInit, AfterViewInit {

  @ViewChild('oc') public overlayContainer: ElementRef;
  @ViewChild('op') public overlayPanel: OverlayPanel;

  private _isVisible: boolean;
  public table: Table;

  public get closeEnabled(): boolean {
    return this.isInputValid();
  }

  constructor(private _behaviourService: EditorBehaviourService,
    private _dataService: EditorDataService) { }

  public ngOnInit(): void {
    this._behaviourService.editTableAction.subscribe(this.startEdit.bind(this));
  }

  public ngAfterViewInit(): void {
    this.overlayPanel.onHide.subscribe(() => this._isVisible = false);
  }

  public update(): void {
    this._behaviourService.updatePlan();
  }

  private startEdit(parameters: EditTableParameters): void {
    if (!this._isVisible) {
      this.displayPanel(parameters);
      this.table = this._dataService.storage.tables[parameters.index];
      if (this.table.number == null) {
        this.table.number = parameters.index + 1;
      }
    }
  }

  private displayPanel(parameters: EditTableParameters): void {
    this._isVisible = true;
    const divElement = this.overlayContainer.nativeElement as HTMLDivElement;
    divElement.style.top = parameters.y.toString() + 'px';
    divElement.style.left = parameters.x - 20 + 'px';
    this.overlayPanel.toggle(null, this.overlayContainer.nativeElement);
  }

  private isInputValid(): boolean {
    return this.table != null &&
      this.table.seats > 0 &&
      this._dataService.storage.tables.length === new Set(this._dataService.storage.tables.map(t => t.number)).size;
  }
}
