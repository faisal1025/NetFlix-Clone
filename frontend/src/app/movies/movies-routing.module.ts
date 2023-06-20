import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from '../service/AuthService/admin.guard';
import { AuthGuard } from '../service/AuthService/auth.guard';
import { FeaturedMoviesComponent } from './featured-movies/featured-movies.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { MyListComponent } from './my-list/my-list.component';
import { NewReleasesMoviesComponent } from './new-releases-movies/new-releases-movies.component';
import { UpcomingMoviesComponent } from './upcoming-movies/upcoming-movies.component';
import { UploadMoviesComponent } from './upload-movies/upload-movies.component';

const MovieRoutes: Routes = [
  {path:"featured", component:FeaturedMoviesComponent, canActivate:[AuthGuard]},
  {path:"newReleases", component:NewReleasesMoviesComponent, canActivate:[AuthGuard]},
  {path:"myList/:id", component:MyListComponent, canActivate:[AuthGuard]},
  {path:"upcoming", component:UpcomingMoviesComponent, canActivate:[AuthGuard]},
  {path:"upload", component:UploadMoviesComponent, canActivate:[AuthGuard, AdminGuard]},
  {path:"movie/:id", component:MovieDetailsComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(MovieRoutes)],
  exports: [RouterModule]
})
export class MoviesRoutingModule { }
