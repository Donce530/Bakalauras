import { TestBed } from '@angular/core/testing';

import { EditorDataService } from './editor-data.service';

describe('EditorDataService', () => {
  let service: EditorDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditorDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
