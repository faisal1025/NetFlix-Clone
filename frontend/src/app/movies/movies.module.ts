import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { MoviesRoutingModule } from './movies-routing.module';
import { FeaturedMoviesComponent } from './featured-movies/featured-movies.component';
import { UpcomingMoviesComponent } from './upcoming-movies/upcoming-movies.component';
import { NewReleasesMoviesComponent } from './new-releases-movies/new-releases-movies.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UploadMoviesComponent } from './upload-movies/upload-movies.component';
import { SharedModule } from '../shared/shared.module';
import { MyListComponent } from './my-list/my-list.component';



@NgModule({
    declarations: [
        MovieDetailsComponent,
        FeaturedMoviesComponent,
        UpcomingMoviesComponent,
        NewReleasesMoviesComponent,
        UploadMoviesComponent,
        MyListComponent
    ],
    exports: [
        MovieDetailsComponent,
        FeaturedMoviesComponent,
        UpcomingMoviesComponent,
        NewReleasesMoviesComponent
    ],
    imports: [
        CommonModule,
        MoviesRoutingModule,
        ReactiveFormsModule,
        NgxPaginationModule,
        SharedModule
    ]
})
export class MoviesModule { }
