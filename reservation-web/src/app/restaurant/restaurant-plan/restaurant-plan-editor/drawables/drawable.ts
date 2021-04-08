import { BoundingBox } from '../models/bounding-box';

export interface Drawable {
    svg: string;
    collidesWith(element: BoundingBox): boolean;
    draw(...contexts: CanvasRenderingContext2D[]): void;
    drawLabel(...contexts: CanvasRenderingContext2D[]): void;
}
