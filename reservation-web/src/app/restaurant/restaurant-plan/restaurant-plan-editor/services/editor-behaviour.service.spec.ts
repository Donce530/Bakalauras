import { TestBed } from '@angular/core/testing';

import { EditorBehaviourService } from './editor-behaviour.service';

describe('EditorBehaviourService', () => {
  let service: EditorBehaviourService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditorBehaviourService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
