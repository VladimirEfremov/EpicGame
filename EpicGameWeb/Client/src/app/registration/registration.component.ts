import { Component, OnInit } from '@angular/core';

import { HttpRegistrationPostData } from '../shared/HttpRegistrationData';
import { HttpAuthService } from '../shared/HttpAuth.service';

export interface HttpPostData {
  FirstName: string;
  SecondName: string;
  PasswordHash: string;
  Nickname: string;
  Email: string;
}

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit 
{
  public PostData: HttpRegistrationPostData = {
    FirstName: '',
    SecondName: '',
    PasswordHash: '',
    Nickname: '',
    Email: ''
  };

  constructor(
    private httpAuth: HttpAuthService) 
  { }

  ngOnInit() {
  }

  onClick() 
  {
      this.httpAuth.registration(this.PostData);
  }


  
}
