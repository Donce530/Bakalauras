import { Drawable } from "../drawables/drawable";
import { Label } from "../drawables/label";
import { InputTextParameters } from '../models/input-text-parameters';
import { BrushBase } from './brush-base';

export class LabelBrush extends BrushBase {

    public askForText: (parameters: InputTextParameters) => void;

    private _label: Label;
    private _enabled = true;

    protected initialize(): void {
        this.previewContext.setLineDash([10, 20]);
        this.previewContext.lineWidth = 2;
        this.previewContext.lineCap = 'round';
        this.previewContext.fillStyle = '#000';
    }

    protected down(): void {
        if (!this._enabled) {
            return;
        }

        this._label = new Label(
            {
                x: this.startX,
                y: this.startY,
            }
        );
    }

    protected up(): void {
        if (!this._enabled) {
            return;
        }

        this._label.width = this.offsetX;
        this._label.height = this.offsetY;

        this._enabled = false;
        this.askForText(new InputTextParameters({
            x: this.absoluteX,
            y: this.absoluteY
        }));
    }

    public onTextInput(text: string): void {
        this._enabled = true;
        this.clear(this.previewContext);
        if (text != null && text.trim().length !== 0) {
            this._label.text = text;
            this._label.sizeToFit(this.drawContext);
            this._label.drawLabel(this.drawContext, this.svgContext);
            this.storage.labels.push(this._label);
        }

        this._label = null;
    }

    protected move(): void {
        if (!this._enabled) {
            return;
        }

        if (this.isDrawing) {
            this.clear(this.previewContext);
            this._label.height = this.offsetY;
            this._label.width = this.offsetX;

            this.previewContext.strokeRect(this._label.x, this._label.y, this._label.width, this._label.height);
        }
    }

    protected leave(): void {
        if (!this._enabled) {
            return;
        }

        if (this.isDrawing) {
            this.clear(this.previewContext);
            this._label = null;
        }
    }

    public draw(drawables: Drawable[]): void {
        drawables.forEach(drawable => {
            drawable.drawLabel(this.drawContext, this.svgContext);
        });
    }
}
