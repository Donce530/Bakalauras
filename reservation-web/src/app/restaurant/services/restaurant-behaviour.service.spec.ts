import { TestBed } from '@angular/core/testing';

import { RestaurantBehaviourService } from './restaurant-behaviour.service';

describe('RestaurantBehaviourService', () => {
  let service: RestaurantBehaviourService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RestaurantBehaviourService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
