import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxPaginationModule } from 'ngx-pagination'; 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomesModule } from './homes/homes.module';
import { HttpClientModule } from '@angular/common/http'
import { MovieApiService } from './service/movie-api.service';
import { MoviesModule } from './movies/movies.module';
import { UsersModule } from './users/users.module';
import { AuthService } from './service/AuthService/auth.service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HomesModule,
    MoviesModule,
    UsersModule,
    NgxPaginationModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [MovieApiService, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
