import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { SearchResultComponent } from './search-result/search-result.component';

const HomeRoutes: Routes = [
  {path:"", component:HomePageComponent},
  {path:"search", component:SearchResultComponent},
];

@NgModule({
  imports: [RouterModule.forChild(HomeRoutes)],
  exports: [RouterModule]
})
export class HomesRoutingModule { }
