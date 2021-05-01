import { TestBed } from '@angular/core/testing';

import { UserManagerDataService } from './user-manager-data.service';

describe('UserManagerDataService', () => {
  let service: UserManagerDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserManagerDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
