import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MovieApiService } from 'src/app/service/movie-api.service';

@Component({
  selector: 'app-new-releases-movies',
  templateUrl: './new-releases-movies.component.html',
  styleUrls: ['./new-releases-movies.component.css']
})
export class NewReleasesMoviesComponent {

  newReleaseMovies:any = [];
  getSearchValue:string = '';
  page:number = 1;
  count: number = 0;
  tableSize: number = 9;
  
  constructor(private movieApi: MovieApiService) {}
  
  ngOnInit(): void {
    this.getNewReleasesMovies();
  }

  getNewReleasesMovies(){
    this.movieApi.getNewReleaseMoviesApiData().subscribe((result)=>{
      console.log(result, 'result#');
      this.newReleaseMovies = result.Data;
    })
  }

  onTextChange(event:string){
    this.getSearchValue = event;
    console.log(this.getSearchValue);
    
  }

  // for pagination
  onTableDataChange(event:any){
    this.page = event;
    this.getNewReleasesMovies();
  }

}
