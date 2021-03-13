
import { Table } from '../drawables/table';
import { Wall } from '../drawables/wall';
import { EditorStorage } from './editor-storage';

export class RestaurantPlan {
    public id: number;
    public webSvg: string;
    public walls: Wall[];
    public tables: Table[];

    constructor(id: number, svg: string, editorStorage: EditorStorage) {
        this.id = id;
        this.webSvg = svg;
        this.tables = editorStorage.tables;
        this.walls = editorStorage.walls;
    }
}
