import { Drawable } from '../drawables/drawable';
import { Table } from '../drawables/table';
import { Wall } from '../drawables/wall';

export class EditorStorage {

    public get drawables(): Drawable[] {
        return [...this.walls, ...this.tables];
    }

    public walls: Wall[] = [];
    public tables: Table[] = [];

    constructor(data?: Partial<EditorStorage>) {
        Object.assign(this, data);
    }
}
