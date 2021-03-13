import { Injectable } from '@angular/core';
import { EditorBehaviourService } from './editor-behaviour.service';

@Injectable()
export class EditorSnappingService {

  public set snappingContext(value: CanvasRenderingContext2D) {
    this._context = value;
    this._context.lineWidth = 1;
    this._context.lineCap = 'square';
    this._context.strokeStyle = '#000';
  }

  public set density(value: number) {
    if (this._context == null) {
      return;
    }

    this._context.clearRect(0, 0, this._context.canvas.width, this._context.canvas.height);
    if (value === 0) {
      return;
    }

    this._density = value;

    this._snapIntervalX = this._context.canvas.width / 16 / this._density;
    this._snapIntervalY = this._context.canvas.height / 9 / this._density;
    for (let i = this._snapIntervalX / 2; i < this._context.canvas.width; i += this._snapIntervalX) {
      for (let j = this._snapIntervalY / 2; j < this._context.canvas.height; j += this._snapIntervalY) {
        this._context.strokeRect(i, j, 1, 1);
      }
    }
  }

  public get density(): number {
    return this._density;
  }

  private _context: CanvasRenderingContext2D;
  private _density = 5;
  private _enabled = false;
  private _available = false;
  private _snapIntervalX: number;
  private _snapIntervalY: number;

  public snapPoint([x, y]: [number, number]): [number, number] {
    const halfIntervalX = this._snapIntervalX / 2;
    const offsetX = x % this._snapIntervalX + halfIntervalX;
    const previousX = x - (x % this._snapIntervalX + halfIntervalX);
    const snappedX = offsetX < halfIntervalX ? previousX : previousX + this._snapIntervalX;

    const halfIntervalY = this._snapIntervalY / 2;
    const offsetY = y % this._snapIntervalY + halfIntervalY;
    const previousY = y - (y % this._snapIntervalY + halfIntervalY);
    const snappedY = offsetY < halfIntervalY ? previousY : previousY + this._snapIntervalY;

    return [snappedX, snappedY];
  }

  constructor(private _behaviourService: EditorBehaviourService) {
    this._behaviourService.snappingScale.subscribe(scale => this.density = scale);
    this._behaviourService.snappingState.subscribe(state => {
      this._enabled = state;
      if (this._available && this._enabled) {
        this.density = this._density;
      } else {
        this.density = 0;
      }
    });
    this._behaviourService.snappingAvailability.subscribe(available => {
      this._available = available;
      if (this._available && this._enabled) {
        this.density = this._density;
      } else {
        this.density = 0;
      }
    });
  }

}
