import { BrushBase } from './brush-base';

export abstract class SnappingBrushBase extends BrushBase {

    public snap: boolean;
    public snapFunction: ([x, y]: [number, number]) => [number, number];
    protected snappedX: number;
    protected snappedY: number;
    protected snapChanged: boolean;

    public onMove(event: MouseEvent): void {
        this.currentX = event.x - this.parentOffsetX;
        this.currentY = event.y - this.parentOffsetY;
        this.offsetX = this.currentX - this.startX;
        this.offsetY = this.currentY - this.startY;
        this.updateSnap();
        this.absoluteX = event.x;
        this.absoluteY = event.y;
        this.move();
    }

    private updateSnap(): void {
        if (this.snap) {
            const [snappedX, snappedY] = this.snapFunction([this.currentX, this.currentY]);
            this.snapChanged = this.snappedX !== snappedX || this.snappedY !== snappedY;

            if (this.snapChanged) {
                this.snappedX = snappedX;
                this.snappedY = snappedY;
            }
        }
    }
}
