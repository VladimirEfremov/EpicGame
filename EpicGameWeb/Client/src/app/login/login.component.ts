import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

//https://github.com/angular/angular/tree/master/packages/common/http/src

export interface HttpPostData {
  login: string;
  password: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit 
{
  public PostData: HttpPostData = {
    login:'', password:''
  };
  
  url: string = "http://httpbin.org/post"; //"http://localhost:2003/Login"

  constructor(private httpClient: HttpClient) { }

  ngOnInit() { }

  onClick() : void
  {
    console.log('login: onClick()');
    this.httpClient.post<HttpPostData>(
      "http://localhost:6430/api/values/post", //"http://localhost:6430/Account/Login", 
      this.PostData).subscribe(data => {
        console.log(data.login, data.password)
      });
  }
}
