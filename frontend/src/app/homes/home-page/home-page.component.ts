import { Component, OnInit } from '@angular/core';
import { MovieApiService } from 'src/app/service/movie-api.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit{

  allMovies:any = [];
  upcomingMovies:any = [];
  newReleaseMovieResult: any = [];
  featuredMovieResult:any = [];

  constructor(private movieApi:MovieApiService) {}

  ngOnInit(): void {
    this.getAllMovies();
  }

  getAllMovies(){
    // this.allMovies = this.movieApi.allMovies;
    // this.featuredMovieResult = this.movieApi.featuredMovies;
    // this.newReleaseMovieResult = this.movieApi.newReleaseMovies;
    // this.upcomingMovies = this.movieApi.upcomingMovies;
    // console.log(this.allMovies);
    
    this.movieApi.getMoviesApiData().subscribe((res)=>{
      console.log(res, "getAllMovies#");
      this.allMovies = res.Data;
      this.upcomingMovies = res.Data1;
      this.newReleaseMovieResult = res.Data2;
      this.featuredMovieResult = res.Data3;
      //this.movieApi.setAllMovies(this.allMovies);
      console.log(this.allMovies, "allmovies#");
      
    })
  }
}
