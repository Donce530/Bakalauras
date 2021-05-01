import { TestBed } from '@angular/core/testing';

import { UserManagerHttpService } from './user-manager-http.service';

describe('UserManagerHttpService', () => {
  let service: UserManagerHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserManagerHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
