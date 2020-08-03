import { TestBed } from '@angular/core/testing';

import { ODataStoreFactoryService } from './odata-store-factory.service';

describe('ODataStoreFactoryService', () => {
  let service: ODataStoreFactoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ODataStoreFactoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
