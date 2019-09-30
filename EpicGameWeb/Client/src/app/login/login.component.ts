import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';

//https://github.com/angular/angular/tree/master/packages/common/http/src

export interface HttpPostData {
  Email: string;
  PasswordHash: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit 
{
  public PostData: HttpPostData = {
    Email:'', PasswordHash:''
  };
  
  url: string = 
    //"http://httpbin.org/post"; 
    "http://localhost:6430/api/auth/post";

  constructor(private httpClient: HttpClient,
              private router: Router) { }
  
  ngOnInit() { }

  onClick() : void
  {
    console.log('login: onClick()');
    let oprions = {
      headers: new HttpHeaders({ 
        'Content-Type': 
          'application/json' })
    }

    this.httpClient.post<HttpPostData>(
      this.url, JSON.stringify(this.PostData), oprions)
      .subscribe(
        data => {
          if (data.toString().length > 0)
          {
              console.log("success");
              let obj = JSON.parse(data);
              if (obj.IsAuthorized)
              {
                  this.router.navigate(['/game']);
              }
          }
        },
        error => console.log(error)
      );

      
  }
}
