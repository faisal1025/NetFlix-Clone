import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from 'src/app/service/AuthService/auth.service';
import { MovieApiService } from 'src/app/service/movie-api.service';
 
@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrls: ['./search-page.component.css']
})
export class SearchPageComponent implements OnInit{

  searchValue:string = '';

  constructor(private movieApi:MovieApiService) {}

  ngOnInit(): void {
    
  }

  @Output() onSearchBoxType = new EventEmitter<string>();

  onSearchBoxEntered(){
    this.onSearchBoxType.emit(this.searchValue);
  }

}
