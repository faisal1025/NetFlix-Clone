import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class MovieApiService {
  
  constructor(private http:HttpClient) { }

  baseUrl = 'https://localhost:5001/api/Movies';
  //sendMovie
  sendMovie(data:any) : Observable<any>{
    console.log(data, "service##");
    
    return this.http.post(`${this.baseUrl}/send`,data);
  }
  
  //getMoviesApiData
  getMoviesApiData(): Observable<any> {
    return this.http.get(`${this.baseUrl}/all`);
  }
  
  //getUpcomingMoviesApiData
  getUpcomingMoviesApiData(): Observable<any>{
    return this.http.get(`${this.baseUrl}/upcoming`);
  }
  
  // getNewReleaseMoviesApiData 
  getNewReleaseMoviesApiData(): Observable<any> {
    return this.http.get(`${this.baseUrl}/newRelease`);
  }
  
  // getFeaturedMoviesApiData 
  getFeaturedMoviesApiData(): Observable<any> {
    return this.http.get(`${this.baseUrl}/featured`);
  }

  getAllMoviesApiData(): Observable<any>{
    return this.http.get(`${this.baseUrl}/allMovies`);
  }
  
  //getMyListMoviesApiData
  getMyListMoviesApiData(data:any): Observable<any>{
    return this.http.get(`${this.baseUrl}/myList/${data}`)
  }
  
  //getMoviesDetailsApiData
  getMoviesDetailsApiData(data:any): Observable<any>{
    return this.http.get(`${this.baseUrl}/movie/${data}`)
  }
  
  addMovieToList(data: any[]): Observable<any>{
    console.log(data, "movieApi Service");
    
    return this.http.post(`${this.baseUrl}/addList`, {
      UserId: data[0],
      MovieId: data[1]
    })
  }
}
