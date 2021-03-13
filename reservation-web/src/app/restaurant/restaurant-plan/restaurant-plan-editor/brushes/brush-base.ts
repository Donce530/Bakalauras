import { Brush } from './brush';
import * as C2S from 'canvas2svg/canvas2svg.js';
import { EditorStorage } from '../models/editor-storage';
import { Drawable } from '../drawables/drawable';

export abstract class BrushBase implements Brush {

    protected isDrawing: boolean;
    protected startX: number;
    protected startY: number;
    protected endX: number;
    protected endY: number;
    protected offsetX: number;
    protected offsetY: number;
    protected ratioX: number;
    protected ratioY: number;
    protected currentX: number;
    protected currentY: number;
    protected parentOffsetX: number;
    protected parentOffsetY: number;
    protected absoluteX: number;
    protected absoluteY: number;

    private _currentSvg: string;

    constructor(protected storage: EditorStorage,
        protected drawContext: CanvasRenderingContext2D,
        protected previewContext: CanvasRenderingContext2D,
        protected svgContext: C2S) {

        const parentBoundingBox = this.drawContext.canvas.parentElement.getBoundingClientRect();
        this.parentOffsetX = parentBoundingBox.x;
        this.parentOffsetY = parentBoundingBox.y;

        this._currentSvg = this.svgContext.getSerializedSvg();

        this.initialize();
    }

    public draw(drawables: Drawable[]): void {
        drawables.forEach(drawable => {
            drawable.draw(this.drawContext, this.svgContext);
        });
    }

    public onDown(event: MouseEvent): void {
        this.isDrawing = true;
        this.startX = event.x - this.parentOffsetX;
        this.startY = event.y - this.parentOffsetY;
        this.down();
    }

    public onUp(event: MouseEvent): void {
        this.endX = event.x - this.parentOffsetX;
        this.endY = event.y - this.parentOffsetY;
        this.up();
        this.isDrawing = false;
        this._currentSvg = this.svgContext.getSerializedSvg();
    }

    public onMove(event: MouseEvent): void {
        this.currentX = event.x - this.parentOffsetX;
        this.currentY = event.y - this.parentOffsetY;
        this.offsetX = this.currentX - this.startX;
        this.offsetY = this.currentY - this.startY;
        this.absoluteX = event.x;
        this.absoluteY = event.y;
        this.move();
    }

    public onLeave(event: MouseEvent): void {
        this.leave();
        this.isDrawing = false;
    }

    protected abstract initialize(): void;
    protected abstract down(): void;
    protected abstract up(): void;
    protected abstract move(): void;
    protected abstract leave(): void;

    protected clear(...contexts: CanvasRenderingContext2D[]): void {
        contexts.forEach(context => {
            context.clearRect(0, 0, context.canvas.width, context.canvas.height);
        });
    }

    protected getNewSvg(updatedSvg: string): string {
        let newSvg = '';
        updatedSvg.split('').forEach((value, i) => {
            if (value !== this._currentSvg.charAt(i)) {
                newSvg += value;
            }
        });

        newSvg = newSvg.substr(0, newSvg.length - 12);
        if (newSvg.startsWith('><')) {
            newSvg = newSvg.substr(2);
        }
        if (newSvg.startsWith('"')) {
            newSvg = newSvg.substr(1);
        }

        return newSvg;
    }
}
