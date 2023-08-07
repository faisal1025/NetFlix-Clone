import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomePageComponent } from './home-page/home-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomesRoutingModule } from './homes-routing.module'
import { MoviesModule } from '../movies/movies.module';
import { SearchResultComponent } from './search-result/search-result.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    HomePageComponent,
    SearchResultComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    HomesRoutingModule,
    ReactiveFormsModule
  ]
})
export class HomesModule { }
