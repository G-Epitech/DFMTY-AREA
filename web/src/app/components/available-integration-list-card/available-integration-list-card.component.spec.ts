import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvailableIntegrationListCardComponent } from './available-integration-list-card.component';

describe('AvailableIntegrationListCardComponent', () => {
  let component: AvailableIntegrationListCardComponent;
  let fixture: ComponentFixture<AvailableIntegrationListCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AvailableIntegrationListCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AvailableIntegrationListCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
