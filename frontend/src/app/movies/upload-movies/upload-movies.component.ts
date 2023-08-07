import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MovieApiService } from 'src/app/service/movie-api.service';
import { __values } from 'tslib';
import { movies } from './movies.models';

@Component({
  selector: 'app-upload-movies',
  templateUrl: './upload-movies.component.html',
  styleUrls: ['./upload-movies.component.css']
})
export class UploadMoviesComponent {
  uploadForm!:FormGroup

  imagePreviewFile:any = "assets/images/default.png";
  videoFile:any;
  imageFile:any;

  constructor(private movieApi:MovieApiService) {}

  ngOnInit():void{
    this.uploadForm = new FormGroup({
      Name: new FormControl(null, [Validators.required]),
      Category: new FormControl(null, [Validators.required, Validators.maxLength(255)]),
      YoR: new FormControl(null, [Validators.required]),
      Starts: new FormControl(null, [Validators.required]),
      Description: new FormControl(null, [Validators.required, Validators.maxLength(8000)]),
      isFeatured: new FormControl(false, [Validators.required]),
      MoviePoster: new FormControl(null, [Validators.required]),
      ContentPath: new FormControl(null, [Validators.required])
    });
  }

  imagePreview(e:any){
    if(e.target.files){
      const reader = new FileReader();
      reader.readAsDataURL(e.target.files[0]);
      this.imageFile = e.target.files[0];
      console.log(this.imageFile);
      
      reader.onload=(event:any)=>{
        this.imagePreviewFile = event.target.result;        
        // console.log(event, "FileReader##");
      }
    } 
  }

  videoSubmit(e:any){
    if(e.target.files){
      this.videoFile = e.target.files[0];
      console.log(this.videoFile)
      // const reader = new FileReader();
      // reader.readAsDataURL(e.target.files[0]);
      // reader.onload=(event:any)=>{
      //   this.videoFile = event.target.result;
      //   console.log(this.videoFile, "file##");
      // }
    }
  }
  onSubmit(){
    debugger
    var formData: FormData = new FormData();
    formData.append('name', this.uploadForm.value.Name);
    formData.append('category', this.uploadForm.value.Category);
    formData.append('yoR', this.uploadForm.value.YoR);
    formData.append('starts', this.uploadForm.value.Starts);
    formData.append('description', this.uploadForm.value.Description);
    formData.append('isFeatured', this.uploadForm.value.isFeatured);
    formData.append('imageName', this.imageFile.name);
    formData.append('videoName', this.videoFile.name);
    formData.append('moviePoster', this.imageFile);
    formData.append('contentPath', this.videoFile);
    console.log(formData);
    this.movieApi.sendMovie(formData).subscribe((res)=>{
      alert(res.MainMessage.Text);
      this.uploadForm.reset();
      console.log(res);
    })
    
  }
}
