import { Component } from '@angular/core';
import { MovieApiService } from 'src/app/service/movie-api.service';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css']
})
export class SearchResultComponent {
  getSearchValue:string = '';
  allMovies:any = [];

  constructor(private movieApi:MovieApiService){}

  ngOnInit():void{
  
  }

  onTextChange(event:string){
    this.getSearchValue = event;
   
    this.movieApi.getSearchedMoviesApiData(this.getSearchValue).subscribe((res)=>{
      this.allMovies = res.Data;
      console.log(this.allMovies);
      
    }, (err)=>{
      console.log(err);
    });
  }
}
