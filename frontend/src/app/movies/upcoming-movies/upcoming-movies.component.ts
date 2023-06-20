import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MovieApiService } from 'src/app/service/movie-api.service';

@Component({
  selector: 'app-upcoming-movies',
  templateUrl: './upcoming-movies.component.html',
  styleUrls: ['./upcoming-movies.component.css']
})
export class UpcomingMoviesComponent {
  upcomingMovies:any = [];
  getSearchValue: string = '';
  page:number = 1;
  count: number = 0;
  tableSize: number = 9;
  constructor(private movieApi: MovieApiService) {}



  ngOnInit(): void {
    this.getUpcomingMovies();
  }

  getUpcomingMovies(){
    this.movieApi.getUpcomingMoviesApiData().subscribe((result)=>{
      console.log(result, 'result#');
      this.upcomingMovies = result.Data;
    })
  }

  onTextChange(event:string){
    this.getSearchValue = event;
    console.log(this.getSearchValue);
    
  }

  // for pagination
  onTableDataChange(event:any){
    this.page = event;
    this.getUpcomingMovies();
  }
  
}
