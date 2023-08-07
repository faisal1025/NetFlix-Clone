import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedRoutingModule } from './shared-routing.module';
import { SearchPageComponent } from './search-page/search-page.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    SearchPageComponent
  ],
  imports: [
    CommonModule,
    SharedRoutingModule,
    FormsModule
  ],
  exports: [
    SearchPageComponent
  ]
})
export class SharedModule { }
