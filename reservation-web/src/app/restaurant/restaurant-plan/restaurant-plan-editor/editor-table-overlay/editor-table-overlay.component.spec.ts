import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorTableOverlayComponent } from './editor-table-overlay.component';

describe('EditorTableOverlayComponent', () => {
  let component: EditorTableOverlayComponent;
  let fixture: ComponentFixture<EditorTableOverlayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditorTableOverlayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditorTableOverlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
