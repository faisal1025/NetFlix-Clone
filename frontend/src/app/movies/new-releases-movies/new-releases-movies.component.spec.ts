import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewReleasesMoviesComponent } from './new-releases-movies.component';

describe('NewReleasesMoviesComponent', () => {
  let component: NewReleasesMoviesComponent;
  let fixture: ComponentFixture<NewReleasesMoviesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewReleasesMoviesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewReleasesMoviesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
