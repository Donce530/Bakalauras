import { BoundingBox } from '../models/bounding-box';
import { Drawable } from './drawable';

export class Table extends BoundingBox implements Drawable {

    public svg: string;
    public seats = 4;
    public number: number;

    public linkedTables: Table[] = [];
    public linkedTableNumbers: number[];

    private _defaultRounding = 10;

    collidesWith(element: BoundingBox): boolean {
        if (element.height > 1 || element.width > 1) {
            console.error('not implemented collision');
            return false;
        } else {
            return element.x >= this.x &&
                element.x <= this.x + this.width &&
                element.y >= this.y &&
                element.y <= this.y + this.height;
        }
    }

    draw(...contexts: CanvasRenderingContext2D[]): void {
        this.ensurePositiveCoordinates();

        let rounding = this._defaultRounding;
        if (Math.abs(this.width) < 2 * this._defaultRounding) {
            rounding = Math.abs(this.width / 2);
        }
        if (Math.abs(this.height) < 2 * this._defaultRounding) {
            rounding = Math.abs(this.height / 2);
        }
        contexts.forEach(context => {
            context.beginPath();
            context.moveTo(this.x + rounding, this.y);
            context.arcTo(this.x + this.width, this.y, this.x + this.width, this.y + this.height, rounding);
            context.arcTo(this.x + this.width, this.y + this.height, this.width, this.y + this.height, rounding);
            context.arcTo(this.x, this.y + this.height, this.x, this.y, rounding);
            context.arcTo(this.x, this.y, this.x + this.width, this.y, rounding);
            context.closePath();
            context.stroke();
        });
    }

    drawLabel(...contexts: CanvasRenderingContext2D[]): void {
        contexts.forEach(context => {
            context.font = `bold ${(this.height + this.width) / 8}px serif`;
            let [midX, midY] = this.middle;
            const textMeasurement = context.measureText(this.number.toString());
            midX = midX - textMeasurement.width / 2;
            midY = midY + (textMeasurement.fontBoundingBoxAscent) / 2;

            context.fillText(this.number.toString(), midX, midY);
        });
    }
}
