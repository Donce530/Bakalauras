import { Table } from '../drawables/table';
import { BoundingBox } from '../models/bounding-box';
import { EditTableParameters } from '../models/edit-table-parameters';
import { SnappingBrushBase } from './snapping-brush-base';

export class TableBrush extends SnappingBrushBase {

    public editTable: (parameters: EditTableParameters) => void;

    private _table: Table;
    private _hoveredTable: Table;
    private _hoveredIndex: number;
    private _hoveredClick = false;

    protected initialize(): void {
        this.drawContext.lineWidth = 5;
        this.drawContext.lineCap = 'round';
        this.drawContext.fillStyle = '#000';
        this.svgContext.lineWidth = 5;
        this.svgContext.lineCap = 'round';
        this.svgContext.fillStyle = '#000';
        this.previewContext.setLineDash([10, 20]);
        this.previewContext.lineWidth = 5;
        this.previewContext.lineCap = 'round';
        this.previewContext.fillStyle = '#000';

        this._table = new Table();
    }

    protected down(): void {
        if (this._hoveredTable) {
            this._hoveredClick = true;
        } else {
            this._table.x = this.snap ? this.snappedX : this.startX;
            this._table.y = this.snap ? this.snappedY : this.startY;
        }
    }

    protected move(): void {

        this.updateHoveredTable();

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
            this._table.height = this.snap ? this.currentY - this._table.y : this.offsetY;
            this._table.width = this.snap ? this.currentX - this._table.x : this.offsetX;

            const previewTable = new Table(this._table);
            previewTable.draw(this.previewContext);
        }
    }

    private updateHoveredTable(): void {
        if (!this.isDrawing) {
            const cursorPosition = new BoundingBox({
                x: this.currentX,
                y: this.currentY
            });

            if (this._hoveredTable?.collidesWith(cursorPosition)) {
                return;
            }

            for (let i = 0; i < this.storage.tables.length; i++) {
                if (this.storage.tables[i].collidesWith(cursorPosition)) {
                    this._hoveredTable = this.storage.tables[i];
                    this._hoveredIndex = i;
                    return;
                }
            }

            this._hoveredTable = null;
            this._hoveredIndex = null;
        }
    }

    protected up(): void {
        if (this._hoveredClick) {
            this.editTable(new EditTableParameters({
                index: this._hoveredIndex,
                x: this.absoluteX,
                y: this.absoluteY
            }));
            this._hoveredClick = false;
            return;
        }

        if (this.isDrawing) {
            this.clear(this.previewContext);
            this._table.width = this.snap ? this.snappedX - this._table.x : this.endX - this._table.x;
            this._table.height = this.snap ? this.snappedY - this._table.y : this.endY - this._table.y;

            if (this._table.width !== 0 || this._table.height !== 0) {
                this._table.draw(this.drawContext, this.svgContext);

                this._table.svg = this.getNewSvg(this.svgContext.getSerializedSvg());
                this._table.number = this.storage.tables.length + 1;
                this.storage.tables.push(this._table);
            }

            this._table = new Table();
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
