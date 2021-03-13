import { Drawable } from '../drawables/drawable';

export interface Brush {
    draw(drawables: Drawable[]): void;
    onDown(event: MouseEvent): void;
    onUp(event: MouseEvent): void;
    onMove(event: MouseEvent): void;
    onLeave(event: MouseEvent): void;
}
