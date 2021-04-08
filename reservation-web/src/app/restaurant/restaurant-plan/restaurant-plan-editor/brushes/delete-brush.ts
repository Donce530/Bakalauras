import { BoundingBox } from '../models/bounding-box';
import { BrushBase } from './brush-base';

export class DeleteBrush extends BrushBase {

    protected initialize(): void { }
    protected down(): void { }
    protected move(): void { }
    protected leave(): void { }
    protected up(): void {
        const cursorBox = new BoundingBox({
            x: this.startX,
            y: this.startY,
            height: 1,
            width: 1
        });

        let deleted = false;
        for (let i = 0; i < this.storage.walls.length; i++) {
            if (this.storage.walls[i].collidesWith(cursorBox)) {
                this.storage.walls.splice(i, 1);
                deleted = true;
                break;
            }
        }
        if (!deleted) {
            for (let i = 0; i < this.storage.tables.length; i++) {
                if (this.storage.tables[i].collidesWith(cursorBox)) {
                    this.storage.tables.splice(i, 1);
                    deleted = true;
                    break;
                }
            }
        }

        if (deleted) {
            this.clear(this.drawContext, this.svgContext);
            this.storage.drawables.forEach(element => {
                element.draw(this.drawContext, this.svgContext);
                element.drawLabel(this.drawContext, this.svgContext);
            });
        }
    }

}
