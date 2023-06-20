import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchPageComponent } from '../shared/search-page/search-page.component';

const SharedRoutes: Routes = [
  
];

@NgModule({
  imports: [RouterModule.forChild(SharedRoutes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }
