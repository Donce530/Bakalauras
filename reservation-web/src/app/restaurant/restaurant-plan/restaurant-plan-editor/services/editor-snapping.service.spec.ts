import { TestBed } from '@angular/core/testing';

import { EditorSnappingService } from './editor-snapping.service';

describe('EditorSnappingService', () => {
  let service: EditorSnappingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditorSnappingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
