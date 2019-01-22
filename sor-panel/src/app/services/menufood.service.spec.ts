import { TestBed } from '@angular/core/testing';

import { MenufoodService } from './menufood.service';

describe('MenufoodService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MenufoodService = TestBed.get(MenufoodService);
    expect(service).toBeTruthy();
  });
});
