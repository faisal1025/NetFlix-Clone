import { Component } from '@angular/core';
import { AuthService } from './service/AuthService/auth.service';
import { MovieApiService } from './service/movie-api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'NetChill';

  constructor(public auth:AuthService, private movieApi:MovieApiService) {}

  ngOnInit():void{ 
    this.auth.getCurrentUser();
    
  }
  
}
