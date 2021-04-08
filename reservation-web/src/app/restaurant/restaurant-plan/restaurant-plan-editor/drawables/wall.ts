import { BoundingBox } from "../models/bounding-box";
import { Drawable } from "./drawable";

export class Wall extends BoundingBox implements Drawable {

    public svg: string;

    collidesWith(element: BoundingBox): boolean {
        if (element.height > 1 || element.width > 1) {
            console.error('not implemented collision');
            return false;
        } else {
            const lineLength = Math.sqrt(Math.pow(this.width, 2) + Math.pow(this.height, 2));
            const d1 = Math.sqrt(Math.pow(element.x - this.x, 2) + Math.pow(element.y - this.y, 2));
            const d2 = Math.sqrt(Math.pow(element.x - this.x - this.width, 2) + Math.pow(element.y - this.y - this.height, 2));
            const buffer = 0.5;
            return d1 + d2 >= lineLength - buffer && d1 + d2 <= lineLength + buffer;
        }
    }

    draw(...contexts: CanvasRenderingContext2D[]): void {
        contexts.forEach(context => {
            context.beginPath();
            context.moveTo(this.x, this.y);
            context.lineTo(this.x + this.width, this.y + this.height);
            context.stroke();
        });
    }

    drawLabel(...contexts: CanvasRenderingContext2D[]): void {

    }
}
