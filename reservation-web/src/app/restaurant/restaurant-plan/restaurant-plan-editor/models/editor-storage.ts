import { Drawable } from '../drawables/drawable';
import { Label } from '../drawables/label';
import { Table } from '../drawables/table';
import { Wall } from '../drawables/wall';

export class EditorStorage {

    public get drawables(): Drawable[] {
        return [...this.walls, ...this.tables, ...this.labels];
    }

    public walls: Wall[] = [];
    public tables: Table[] = [];
    public labels: Label[] = [];

    constructor(data?: Partial<EditorStorage>) {
        Object.assign(this, data);
    }
}
