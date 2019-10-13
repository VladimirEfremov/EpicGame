import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';

import { HttpLoginPostData } from '../shared/HttpLoginPostData';
import { HttpAuthService } from '../shared/HttpAuth.service';

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
  
  ngOnInit() 
  { 
  }

  onClick() : void
  {
      console.log("LOGIN COMPONENT: " +
        "nickname: " + this.LoginPostData.Nickname + 
        " pass: " + this.LoginPostData.PasswordHash);
      this.httpAuth.login(this.LoginPostData);
  }
}
