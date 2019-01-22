import { TestBed } from '@angular/core/testing';

import { WordclockService } from './wordclock.service';

describe('WordclockService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WordclockService = TestBed.get(WordclockService);
    expect(service).toBeTruthy();
  });
});
