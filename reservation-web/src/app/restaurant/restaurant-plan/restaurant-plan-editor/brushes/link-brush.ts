import { Table } from '../drawables/table';
import { Wall } from '../drawables/wall';
import { BoundingBox } from '../models/bounding-box';
import { BrushBase } from './brush-base';

export class LinkBrush extends BrushBase {

    private _firstTable: Table;
    private _secondTable: Table;

    protected initialize(): void {
        this.drawTableLinks();
        console.log(this.previewContext);
    }

    protected down(): void {
        if (this.lastClickedButton === 2) {
            this.deleteLink();
        } else {
            this._firstTable = this.getSelected();
        }
    }

    protected move(): void {
        if (this.isDrawing && this._firstTable != null && this.lastClickedButton !== 2) {
            console.log(JSON.parse(JSON.stringify(this.previewContext)));
            this._secondTable = this.getSelected();
            if (this._firstTable !== this._secondTable) {
                this.clear(this.previewContext);
                this.drawTableLinks();
                this.setPreviewSettings();
                this.previewLink(this._firstTable, this._secondTable);
            }
        }
    }

    protected leave(): void {
        this._firstTable = null;
        this._secondTable = null;
        this.clear(this.previewContext);
        this.drawTableLinks();
    }

    protected up(): void {
        if (this.lastClickedButton !== 2 &&
            this._firstTable != null &&
            this._secondTable != null &&
            this._firstTable !== this._secondTable &&
            !this._firstTable.linkedTables.includes(this._secondTable) &&
            !this._secondTable.linkedTables.includes(this._firstTable)) {

            this._firstTable.linkedTables.push(this._secondTable);
            this._secondTable.linkedTables.push(this._firstTable);
        }

        this.clear(this.previewContext);
        this.drawTableLinks();
    }

    private drawTableLinks(): void {
        this.setPermanentSettings();
        this.storage.tables.forEach(table => {
            table.linkedTables.forEach(linkedTable => {
                this.previewLink(table, linkedTable);
            });
        });
    }

    private getSelected(): Table {
        const cursorBox = this.getCurrentCursorBox();

        for (const table of this.storage.tables) {
            if (table.collidesWith(cursorBox)) {
                return table;
            }
        }

        return null;
    }

    private setPreviewSettings(): void {
        this.previewContext.setLineDash([10, 20]);
        this.previewContext.lineWidth = 5;
        this.previewContext.lineCap = 'round';
        this.previewContext.strokeStyle = '#000';
    }

    private setPermanentSettings(): void {
        this.previewContext.setLineDash([10, 20]);
        this.previewContext.lineWidth = 5;
        this.previewContext.lineCap = 'round';
        this.previewContext.strokeStyle = 'green';
    }

    private previewLink(firstTable: Table, secondTable: Table): void {
        this.previewContext.beginPath();
        const [startX, startY] = firstTable.middle;
        this.previewContext.moveTo(startX, startY);
        const [endX, endY] = secondTable != null ?
            secondTable.middle :
            [this.currentX, this.currentY];
        this.previewContext.lineTo(endX, endY);
        this.previewContext.stroke();
    }

    private deleteLink(): void {
        const cursorBox = this.getCurrentCursorBox();

        for (const table of this.storage.tables) {
            const firstTableMidpoint = table.middle;
            for (const linkedTable of table.linkedTables) {
                const linkedTableMidpoint = linkedTable.middle;
                const tempWall = new Wall({
                    x: firstTableMidpoint[0],
                    y: firstTableMidpoint[1],
                    height: linkedTableMidpoint[1] - firstTableMidpoint[1],
                    width: linkedTableMidpoint[0] - firstTableMidpoint[0]
                });

                if (tempWall.collidesWith(cursorBox)) {
                    table.linkedTables = table.linkedTables.filter(lt => lt !== linkedTable);
                    linkedTable.linkedTables = linkedTable.linkedTables.filter(lt => lt !== table);

                    this.clear(this.previewContext);
                    this.drawTableLinks();
                    return;
                }
            }
        }
    }

    private getCurrentCursorBox(): BoundingBox {
        return new BoundingBox({
            x: this.currentX,
            y: this.currentY,
            height: 1,
            width: 1
        });
    }
}
