export class BoundingBox {
    public id: number;
    public x: number;
    public y: number;
    public width: number;
    public height: number;

    constructor(data?: Partial<BoundingBox>) {
        Object.assign(this, data);
    }

    protected ensurePositiveCoordinates(): void {
        if (this.height > 0 && this.width > 0) {
            return;
        }

        if (this.height > 0 && this.width < 0) {
            this.x = this.x + this.width;
            this.width = this.width * -1;
        }

        if (this.height < 0 && this.width > 0) {
            this.y = this.y + this.height;
            this.height = this.height * -1;
        }

        if (this.height < 0 && this.width < 0) {
            this.x = this.x + this.width;
            this.width = this.width * -1;
            this.y = this.y + this.height;
            this.height = this.height * -1;
        }
    }
}
