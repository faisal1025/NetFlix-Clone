import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MovieApiService } from 'src/app/service/movie-api.service';

@Component({
  selector: 'app-featured-movies',
  templateUrl: './featured-movies.component.html',
  styleUrls: ['./featured-movies.component.css']
})

export class FeaturedMoviesComponent implements OnInit{

  featuredMovies:any = [];
  getSearchValue:string = '';
  page:number = 1;
  count: number = 0;
  tableSize: number = 3;

  constructor(private movieApi: MovieApiService) {}

  
  ngOnInit(): void {
    this.getFeaturedMovies();
  }
  

  getFeaturedMovies(){
    this.movieApi.getFeaturedMoviesApiData().subscribe((result)=>{
      console.log(result, 'result#');
      this.featuredMovies = result.Data;
    })
  }

  // for search
  onTextChange(event:string){
    this.getSearchValue = event;
  }

  // for pagination
  onTableDataChange(event:any){
    this.page = event;
    this.getFeaturedMovies();
  }

}
