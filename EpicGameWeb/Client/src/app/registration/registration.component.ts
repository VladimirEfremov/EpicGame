import { Component, OnInit } from '@angular/core';

import { HttpRegistrationPostData } from '../shared/HttpRegistrationData';
import { HttpAuthService } from '../shared/HttpAuth.service';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  providers: [HttpAuthService]
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