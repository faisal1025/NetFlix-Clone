import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevokeSubscriptionComponent } from './revoke-subscription.component';

describe('RevokeSubscriptionComponent', () => {
  let component: RevokeSubscriptionComponent;
  let fixture: ComponentFixture<RevokeSubscriptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevokeSubscriptionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RevokeSubscriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
