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

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
  }

  onClick() {
    alert('on click function works!')
    this.httpClient.post(
      "http://localhost:6430/Account/Registration", 
      this.PostData);
  }
}
