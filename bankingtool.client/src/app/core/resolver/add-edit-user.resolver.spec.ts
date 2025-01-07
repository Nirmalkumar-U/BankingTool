import { TestBed } from '@angular/core/testing';

import { AddEditUserResolver } from './add-edit-user.resolver';

describe('AddEditUserResolver', () => {
  let resolver: AddEditUserResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(AddEditUserResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
