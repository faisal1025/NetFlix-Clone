import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/service/AuthService/auth.service';
import { MovieApiService } from 'src/app/service/movie-api.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit{
  
  constructor(private movieApi:MovieApiService, private router:ActivatedRoute, private auth: AuthService) {}
  getMovieDetailResult:any;
  relatedMovieResult:any = [];
  geners:any = [];
  
  ngOnInit(): void {
    let getParamId = this.router.snapshot.paramMap.get('id');
    
    this.getMovie(getParamId);
  }
  
  addList(Id:number) {
    console.log("addList works");
    
    this.movieApi.addMovieToList([
      Number(this.auth.currentUser.value.Id),Id
    ]).subscribe((res)=>{
      console.log(res);
      if(res.IsSuccess){
        alert(res.MainMessage.Text);
      }else{
        alert("Movie not added. Something went wrong!")
      }
    })
  }


  getMovie(id:any){
    this.movieApi.getMoviesDetailsApiData(id).subscribe((result)=>{
      console.log(result, 'result#');
      this.getMovieDetailResult = result.Data;
      this.getMovieDetailResult.YoR = this.getMovieDetailResult?.YoR.slice(0, 4);
      if(this.getMovieDetailResult.Category == "Upcoming"){
        this.movieApi.getUpcomingMoviesApiData().subscribe((res)=>{
          this.relatedMovieResult = res.Data;
        })
      }else if(this.getMovieDetailResult.Category == "New Release"){
        this.movieApi.getNewReleaseMoviesApiData().subscribe((res)=>{
          this.relatedMovieResult = res.Data;
        })
      }else{
        this.movieApi.getFeaturedMoviesApiData().subscribe((res)=>{
          this.relatedMovieResult = res.Data;
        })
      }
    });
  }
}
