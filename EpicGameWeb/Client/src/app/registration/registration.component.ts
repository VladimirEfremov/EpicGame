import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface HttpPostData {
  firstName: string;
  secondName: string;
  passwordHash: string;
  nickname: string;
  email: string;
}

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit 
{
  public PostData: HttpPostData = {
    firstName: '',
    secondName: '',
    passwordHash: '',
    nickname: '',
    email: ''
  };

  url: string = 
    //"http://httpbin.org/post"; 
    "http://localhost:6430/api/home/index";

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
  }

  onClick() 
  {
      console.log('registration: onClick()');
      let oprions = {
        headers: new HttpHeaders({ 
          'Content-Type': 
            'application/json' })
      }
  
      this.httpClient.post<HttpPostData>(
        this.url, JSON.stringify(this.PostData), oprions)
        .subscribe(
          data => {
            console.log(
              "success: " + 
              `${data.json.firstName} ${data.json.secondName} ${data.json.passwordHash} ${data.json.nickname} ${data.json.email}`)
          },
          error => console.log(error)
        );

  }


  
}
