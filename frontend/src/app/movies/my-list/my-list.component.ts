
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/service/AuthService/auth.service';
import { MovieApiService } from 'src/app/service/movie-api.service';

@Component({
  selector: 'app-my-list',
  templateUrl: './my-list.component.html',
  styleUrls: ['./my-list.component.css']
})
export class MyListComponent implements OnInit{
  getSearchValue:string='';
  myListMovies:any = [];
  getParamId:any;
  page:number = 1;
  count: number = 0;
  tableSize: number = 9;
  constructor(private movieApi: MovieApiService, private auth:AuthService, private router:ActivatedRoute) {}
  

  ngOnInit():void{
    this.getParamId = this.router.snapshot.paramMap.get('id');
    this.getMyListMovies(this.getParamId);
  }

  getMyListMovies(data:any){

    this.movieApi.getMyListMoviesApiData(Number(data)).subscribe((result)=>{
      console.log(result, 'listData#');
      this.myListMovies = result.Data;
    })
  }

  onTextChange(event:string){
    this.getSearchValue = event;
    console.log(this.getSearchValue);
    
  }

  // for pagination
  onTableDataChange(event:any){
    this.page = event;
    this.getMyListMovies(this.getParamId);
  }

}
