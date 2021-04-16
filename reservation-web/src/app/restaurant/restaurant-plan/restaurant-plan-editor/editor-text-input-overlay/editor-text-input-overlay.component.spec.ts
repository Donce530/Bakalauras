import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorTextInputOverlayComponent } from './editor-text-input-overlay.component';

describe('EditorTextInputOverlayComponent', () => {
  let component: EditorTextInputOverlayComponent;
  let fixture: ComponentFixture<EditorTextInputOverlayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditorTextInputOverlayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditorTextInputOverlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
