import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
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
    
    return this.http.post(`${this.baseUrl}/send`,data, {
      headers: new HttpHeaders({
        "Authorization": `bearer ${localStorage.getItem("tokenAccess")}`
      })
    });
  }
  
  //getMoviesApiData
  getMoviesApiData(): Observable<any> {
    return this.http.get(`${this.baseUrl}/all`);
  }
  
  //getUpcomingMoviesApiData
  getUpcomingMoviesApiData(): Observable<any>{
    return this.http.get(`${this.baseUrl}/upcoming`, {
      headers: new HttpHeaders({
        "Authorization": `bearer ${localStorage.getItem("tokenAccess")}`
      })
    });
  }
  
  // getNewReleaseMoviesApiData 
  getNewReleaseMoviesApiData(): Observable<any> {
    return this.http.get(`${this.baseUrl}/newRelease`, {
      headers: new HttpHeaders({
        "Authorization": `bearer ${localStorage.getItem("tokenAccess")}`
      })
    });
  }
  
  // getFeaturedMoviesApiData 
  getFeaturedMoviesApiData(): Observable<any> {
    return this.http.get(`${this.baseUrl}/featured`, {
      headers: new HttpHeaders({
        "Authorization": `bearer ${localStorage.getItem("tokenAccess")}`
      })
    });
  }

  getAllMoviesApiData(): Observable<any>{
    return this.http.get(`${this.baseUrl}/allMovies`);
  }

  getSearchedMoviesApiData(value: String): Observable<any>{
    return this.http.get(`${this.baseUrl}/search/${value}`);
  }
  
  //getMyListMoviesApiData
  getMyListMoviesApiData(data:any): Observable<any>{
    return this.http.get(`${this.baseUrl}/myList/${data}`, {
      headers: new HttpHeaders({
        "Authorization": `bearer ${localStorage.getItem("tokenAccess")}`
      })
    })
  }
  
  //getMoviesDetailsApiData
  getMoviesDetailsApiData(data:any): Observable<any>{
    return this.http.get(`${this.baseUrl}/movie/${data}`, {
      headers: new HttpHeaders({
        "Authorization": `bearer ${localStorage.getItem("tokenAccess")}`
      })
    })
  }
  
  addMovieToList(data: any[]): Observable<any>{
    console.log(data, "movieApi Service");
    
    return this.http.post(`${this.baseUrl}/addList`, {
      UserId: data[0],
      MovieId: data[1]
    }, {
      headers: new HttpHeaders({
        "Authorization": `bearer ${localStorage.getItem("tokenAccess")}`
      })
    })
  }
}
