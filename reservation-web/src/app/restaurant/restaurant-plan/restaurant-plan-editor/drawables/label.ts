import { BoundingBox } from "../models/bounding-box";
import { Drawable } from "./drawable";

export class Label extends BoundingBox implements Drawable {
    svg: string;

    public text: string;
    public fontSize: number;

    public collidesWith(element: BoundingBox): boolean {
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

    public draw(...contexts: CanvasRenderingContext2D[]): void {
        this.ensurePositiveCoordinates();


    }

    drawLabel(...contexts: CanvasRenderingContext2D[]): void {
        this.ensurePositiveCoordinates();

        contexts.forEach(context => {
            this.fontSize = this.height;

            context.font = `${this.fontSize}px arial`;
            const textMeasurement = context.measureText(this.text);

            let [midX, midY] = this.middle;
            midX = midX - textMeasurement.width / 2;
            midY = midY + (textMeasurement.fontBoundingBoxAscent) / 4;
            context.fillText(this.text, midX, midY);
        });
    }

    public sizeToFit(context: CanvasRenderingContext2D): void {
        let needsRefit = true;
        let textMeasurement: TextMetrics;

        this.fontSize = 100;
        while (needsRefit) {
            context.font = `${this.fontSize}px arial`;
            textMeasurement = context.measureText(this.text);
            needsRefit = textMeasurement.width > this.width || textMeasurement.fontBoundingBoxAscent > this.height;
            this.fontSize = this.fontSize * 0.9;
        }

        let [midX, midY] = this.middle;
        midX = midX - textMeasurement.width / 2;
        midY = midY + (textMeasurement.fontBoundingBoxAscent) / 4;

        this.x = midX;
        this.y = midY - textMeasurement.fontBoundingBoxAscent * 3 / 4;
        this.width = textMeasurement.width;
        this.height = textMeasurement.fontBoundingBoxAscent;
    }
}
