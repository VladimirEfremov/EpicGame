import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { HttpLoginPostData } from './HttpLoginPostData';
import { HttpRegistrationPostData } from './HttpRegistrationData';

import { Md5 } from 'ts-md5/dist/md5';
import { AccountData } from './AccountData';
import { Observable } from 'rxjs';

@Injectable()
export class HttpAuthService
{
    loginUrl: string = 
        //"http://httpbin.org/post"; 
        "http://localhost:6430/Auth/Login";

    registrationUrl: string = 
        "http://localhost:6430/Auth/Registration";
     
    constructor(
        private httpClient: HttpClient,
        private router: Router) 
    { }

    login(loginPostData: HttpLoginPostData) : void 
    {
        loginPostData.PasswordHash = 
            Md5.hashStr(loginPostData.PasswordHash)
            .toString();

        console.log('login');
        let oprions = {
        headers: new HttpHeaders({ 
            'Content-Type': 'application/json'})
        }

        console.log("login [post]: " + 
            loginPostData.Nickname 
            + " " + 
            loginPostData.PasswordHash);

        let dataToPost = JSON.stringify(loginPostData);
        console.log("data to post: " + dataToPost);

        this.httpClient.post<HttpLoginPostData>(
            this.loginUrl, 
            dataToPost, 
            oprions)
            .subscribe(
                data => {
                    if (data.toString().length > 0)
                    {
                        console.log("login success [response: "+data.toString()+"]");
                        if (data.toString() === "true")
                        {
                            console.log("routing to a game");
                            this.router.navigate(['/game-menu']);
                        }
                        else 
                        {
                            console.log("obj isnt correct [routing to a login]");
                            this.router.navigate(['/login']);
                        }
                    }
                },
                error => console.log(error)
            );
    }

    registration(registrationPostData: HttpRegistrationPostData)
    {
        registrationPostData.PasswordHash = 
            Md5.hashStr(registrationPostData.PasswordHash)
            .toString();

        console.log('registration');
        let oprions = {
            headers: new HttpHeaders({ 
            'Content-Type': 
                'application/json' })
        }
        
        let dataToPost = JSON.stringify(registrationPostData);
        console.log("data to post: " + dataToPost);

        this.httpClient.post<HttpRegistrationPostData>(
            this.registrationUrl, 
            dataToPost, oprions)
            .subscribe(
                data => {
                if (data.toString().length > 0)
                {
                    console.log("registration success [response: "+data.toString()+"]");
                    if (data.toString() === "true")
                    {
                        this.router.navigate(['/game-menu']);
                    }
                    else 
                    {
                        this.router.navigate(['/registration']);
                    }
                }
                },
                error => console.log(error)
            );
    }

    GetAccountData() : AccountData
    {
        let response : AccountData; 
        this.httpClient.get<AccountData>(
            "http://localhost:6430/Auth/GetAccountData")
            .subscribe(
                (data: AccountData) => {
                    console.log("data: " + data.toString());
                    response = data;
                }
            );
        console.log("response [GetAccountData]: " + 
            response.Nickname + " " + response.FriendsList); 
        return response;

    }



}
