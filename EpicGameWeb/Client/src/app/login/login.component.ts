import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { HttpLoginPostData } from '../shared/HttpLoginPostData';
import { HttpAuthService } from '../shared/HttpAuth.service';


export interface HttpPostData {
  Nickname: string;
  PasswordHash: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [HttpAuthService]
})
export class LoginComponent implements OnInit 
{
  LoginPostData: HttpLoginPostData = 
  {
      Nickname: '',
      PasswordHash: ''
  };

  constructor(private httpAuth: HttpAuthService) { }
  
  ngOnInit() { }

  onClick() : void
  {
      this.httpAuth.login(this.LoginPostData);
  }
}
