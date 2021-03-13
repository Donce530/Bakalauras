import { Wall } from '../drawables/wall';
import { SnappingBrushBase } from './snapping-brush-base';

export class WallBrush extends SnappingBrushBase {

    private wall: Wall;

    protected initialize(): void {
        this.drawContext.lineWidth = 5;
        this.drawContext.lineCap = 'round';
        this.drawContext.strokeStyle = '#000';
        this.svgContext.lineWidth = 5;
        this.svgContext.lineCap = 'round';
        this.svgContext.strokeStyle = '#000';
        this.previewContext.setLineDash([10, 20]);
        this.previewContext.lineWidth = 5;
        this.previewContext.lineCap = 'round';
        this.previewContext.strokeStyle = '#000';

        this.wall = new Wall();
    }

    protected down(): void {
        this.wall.x = this.snap ? this.snappedX : this.startX;
        this.wall.y = this.snap ? this.snappedY : this.startY;
    }

    protected move(): void {
        let cleared = false;

        if (this.snap && this.snapChanged) {
            this.clear(this.previewContext);
            cleared = true;
            this.previewSnap();
        }

        if (this.isDrawing) {
            if (!cleared) {
                this.clear(this.previewContext);
                if (this.snap) {
                    this.previewSnap();
                }
            }
            this.wall.height = this.snap ? this.currentY - this.wall.y : this.offsetY;
            this.wall.width = this.snap ? this.currentX - this.wall.x : this.offsetX;
            this.wall.draw(this.previewContext);
        }
    }

    protected up(): void {
        if (this.isDrawing) {
            this.clear(this.previewContext);
            this.wall.width = this.snap ? this.snappedX - this.wall.x : this.endX - this.wall.x;
            this.wall.height = this.snap ? this.snappedY - this.wall.y : this.endY - this.wall.y;

            if (this.wall.width !== 0 || this.wall.height !== 0) {
                this.wall.draw(this.drawContext, this.svgContext);

                this.wall.svg = this.getNewSvg(this.svgContext.getSerializedSvg());
                this.storage.walls.push(this.wall);
            }

            this.wall = new Wall();
        }
    }

    protected leave(): void {
        this.clear(this.previewContext);
    }

    private previewSnap(): void {
        const previousStrokeStyle = this.previewContext.fillStyle;
        this.previewContext.strokeStyle = '#009402';
        this.previewContext.strokeRect(this.snappedX, this.snappedY, 1, 1);
        this.previewContext.strokeStyle = previousStrokeStyle;
    }
}
