import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { OverlayPanel } from 'primeng/overlaypanel';
import { InputTextParameters } from '../models/input-text-parameters';
import { EditorBehaviourService } from '../services/editor-behaviour.service';

@Component({
  selector: 'app-editor-text-input-overlay',
  templateUrl: './editor-text-input-overlay.component.html',
  styleUrls: ['./editor-text-input-overlay.component.css']
})
export class EditorTextInputOverlayComponent implements OnInit {

  @ViewChild('oc') public overlayContainer: ElementRef;
  @ViewChild('op') public overlayPanel: OverlayPanel;

  public input: string;

  constructor(private _behaviourService: EditorBehaviourService) { }

  public ngOnInit(): void {
    this._behaviourService.openTextInputAction.subscribe(this.startEdit.bind(this));
  }

  public onHide(): void {
    this._behaviourService.closeTextInput(this.input);
    this.input = null;
  }

  private startEdit(parameters: InputTextParameters): void {
    this.displayPanel(parameters);
  }

  private displayPanel(parameters: InputTextParameters): void {
    const divElement = this.overlayContainer.nativeElement as HTMLDivElement;
    divElement.style.top = parameters.y.toString() + 'px';
    divElement.style.left = parameters.x - 20 + 'px';
    this.overlayPanel.show(null, this.overlayContainer.nativeElement);
  }
}
