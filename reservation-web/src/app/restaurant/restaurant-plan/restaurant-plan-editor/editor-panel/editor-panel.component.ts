import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Brush } from '../brushes/brush';
import { EditorMode } from '../enums/editor-mode.enum';
import { EditorBehaviourService } from '../services/editor-behaviour.service';
import * as C2S from 'canvas2svg/canvas2svg.js';
import { WallBrush } from '../brushes/wall-brush';
import { combineLatest, fromEvent, Subject, SubscriptionLike } from 'rxjs';
import { EditorSnappingService } from '../services/editor-snapping.service';
import { DeleteBrush } from '../brushes/delete-brush';
import { TableBrush } from '../brushes/table-brush';
import { RestaurantDataService } from 'src/app/restaurant/services/restaurant-data.service';
import { EditTableParameters } from '../models/edit-table-parameters';
import { EditorDataService } from '../services/editor-data.service';
import { EditorStorage } from '../models/editor-storage';
import { map } from 'rxjs/operators';
import { RestaurantBehaviourService } from 'src/app/restaurant/services/restaurant-behaviour.service';
import { LinkBrush } from '../brushes/link-brush';
import { LabelBrush } from '../brushes/label-brush';
import { InputTextParameters } from '../models/input-text-parameters';

@Component({
  selector: 'app-editor-panel',
  templateUrl: './editor-panel.component.html',
  styleUrls: ['./editor-panel.component.css']
})
export class EditorPanelComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild('planCanvas') public planCanvas: ElementRef;
  @ViewChild('previewCanvas') public previewCanvas: ElementRef;
  @ViewChild('snappingCanvas') public snappingCanvas: ElementRef;
  @ViewChild('outerWrapper') public outerWrapper: ElementRef;
  @ViewChild('box') public box: ElementRef;

  public brush: Brush;

  private _planContext: CanvasRenderingContext2D;
  private _previewContext: CanvasRenderingContext2D;
  private _svgContext = new C2S(2160, 3840);

  private _contextReady = new Subject<void>();
  private _brushSubsciptions: SubscriptionLike[] = [];

  constructor(private _behaviourService: EditorBehaviourService,
    private _editorDataService: EditorDataService,
    private _snappingService: EditorSnappingService,
    private _restaurantDataService: RestaurantDataService,
    private _restaurantBehaviourService: RestaurantBehaviourService) { }

  public ngOnInit(): void {
    this.initializePlan();
    this._behaviourService.editorMode.subscribe(this.changeBrush.bind(this));
    this._behaviourService.saveAction.subscribe(this.save.bind(this));
  }

  public ngAfterViewInit(): void {
    this._planContext = this.setupContext(this.planCanvas);
    this._previewContext = this.setupContext(this.previewCanvas);
    const snappingContext = this.setupContext(this.snappingCanvas);

    setTimeout(() => {
      const nativeOuterWrapper = this.outerWrapper.nativeElement as HTMLDivElement;
      const nativeBox = this.box.nativeElement as HTMLDivElement;
      nativeBox.style.height = nativeOuterWrapper.offsetHeight + 'px';

      const ratioX = this._previewContext.canvas.width / this._previewContext.canvas.offsetWidth;
      const ratioY = this._previewContext.canvas.height / this._previewContext.canvas.offsetHeight;

      this._planContext.scale(ratioX, ratioY);
      this._previewContext.scale(ratioX, ratioY);
      this._svgContext.scale(ratioX, ratioY);

      snappingContext.scale(ratioX, ratioY);
      this._snappingService.snappingContext = snappingContext;

      this._contextReady.next();
    }, 1);
  }

  public ngOnDestroy(): void {
    this._brushSubsciptions.forEach(subscription => subscription?.unsubscribe());
  }

  private setupContext(canvas: ElementRef): CanvasRenderingContext2D {
    const canvasElement: HTMLCanvasElement = canvas.nativeElement;
    canvasElement.width = 3840;
    canvasElement.height = 2160;

    return canvasElement.getContext('2d');
  }

  private changeBrush(mode: EditorMode): void {
    const canvas: HTMLCanvasElement = this.previewCanvas.nativeElement;
    this._brushSubsciptions.forEach(subscription => subscription?.unsubscribe());
    let supportsSnapping = false;
    switch (mode) {
      case EditorMode.Wall: {
        const brush = new WallBrush(this._editorDataService.storage, this._planContext, this._previewContext, this._svgContext);
        brush.snapFunction = (point: [number, number]) => this._snappingService.snapPoint(point);
        this._brushSubsciptions.push(this._behaviourService.snappingState.subscribe(state => brush.snap = state));
        supportsSnapping = true;
        this.brush = brush;
        break;
      }
      case EditorMode.Table: {
        const brush = new TableBrush(this._editorDataService.storage, this._planContext, this._previewContext, this._svgContext);
        brush.snapFunction = (point: [number, number]) => this._snappingService.snapPoint(point);
        this._brushSubsciptions.push(this._behaviourService.snappingState.subscribe(state => brush.snap = state));
        brush.editTable = (parameters: EditTableParameters) => this._behaviourService.initTableEdit(parameters);
        supportsSnapping = true;
        this.brush = brush;
        break;
      }
      case EditorMode.Link: {
        const brush = new LinkBrush(this._editorDataService.storage, this._planContext, this._previewContext, this._svgContext);
        this.brush = brush;
        break;
      }
      case EditorMode.Comment: {
        const brush = new LabelBrush(this._editorDataService.storage, this._planContext, this._previewContext, this._svgContext);
        this._brushSubsciptions.push(this._behaviourService.closeTextInputAction.subscribe(text => brush.onTextInput(text)));
        brush.askForText = (parameters: InputTextParameters) => this._behaviourService.openTextInput(parameters);
        this.brush = brush;
        break;
      }
      case EditorMode.Delete: {
        this.brush = new DeleteBrush(this._editorDataService.storage, this._planContext, this._previewContext, this._svgContext);
        break;
      }
      case EditorMode.None: {
        this.brush = null;
        break;
      }
    }

    this._behaviourService.setSnappingAvailability(supportsSnapping);

    if (this.brush) {
      this._brushSubsciptions.push(
        fromEvent(canvas, 'mousedown').subscribe(mouseEvent => this.brush.onDown(mouseEvent as MouseEvent)),
        fromEvent(canvas, 'mouseup').subscribe(mouseEvent => this.brush.onUp(mouseEvent as MouseEvent)),
        fromEvent(canvas, 'mousemove').subscribe(mouseEvent => this.brush.onMove(mouseEvent as MouseEvent)),
        fromEvent(canvas, 'mouseleave').subscribe(mouseEvent => this.brush.onLeave(mouseEvent as MouseEvent))
      );
    }
  }

  private initializePlan(): void {

    this._editorDataService.storage = new EditorStorage({
      walls: [],
      tables: [],
      labels: []
    });

    combineLatest([this._restaurantDataService.getPlan(), this._contextReady.asObservable()])
      .pipe(map(combineResults => combineResults[0])).subscribe(plan => {
        this._editorDataService.planId = plan.id;
        this._editorDataService.storage = new EditorStorage({
          walls: plan.walls,
          tables: plan.tables,
          labels: plan.labels
        });


        this.changeBrush(EditorMode.Table);
        this.brush.draw(this._editorDataService.storage.tables);

        this.changeBrush(EditorMode.Wall);
        this.brush.draw(this._editorDataService.storage.walls);

        this.changeBrush(EditorMode.Comment);
        this.brush.draw(this._editorDataService.storage.labels);

        this.changeBrush(EditorMode.None);
      });
  }

  private save(): void {
    this._editorDataService.svg = this._svgContext.getSerializedSvg();
    const plan = this._editorDataService.plan;
    this._restaurantDataService.savePlan(plan).subscribe(
      () => this._restaurantBehaviourService.setEditorOpenState(false)
    );
  }
}
