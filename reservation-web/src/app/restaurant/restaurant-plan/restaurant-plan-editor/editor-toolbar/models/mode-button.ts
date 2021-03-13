import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { EditorMode } from '../../enums/editor-mode.enum';

export class ModeButton {
    public tooltip: string;
    public icon: IconDefinition;
    public mode: EditorMode;

    constructor(data: Partial<ModeButton>) {
        Object.assign(this, data);
    }
}
