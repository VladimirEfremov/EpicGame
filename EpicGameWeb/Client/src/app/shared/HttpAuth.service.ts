import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { HttpLoginPostData } from './HttpLoginPostData';
import { HttpRegistrationPostData } from './HttpRegistrationData';

import { Md5 } from 'ts-md5/dist/md5';

@Injectable()
export class HttpAuthService
{
    loginUrl: string = 
        //"http://httpbin.org/post"; 
        "http://localhost:6430/api/auth/login";

    registrationUrl: string = 
        "http://localhost:6430/api/auth/registration";
     
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
            'Content-Type': 
            'application/json' })
        }

        this.httpClient.post<HttpLoginPostData>(
        this.loginUrl, 
        JSON.stringify(loginPostData), 
        oprions)
        .subscribe(
            data => {
                if (data.toString().length > 0)
                {
                    console.log("success");
                    let obj = JSON.parse(data.toString());
                    if (obj.IsCorrect)
                    {
                        this.router.navigate(['/game']);
                    }
                    else 
                    {
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
        
        this.httpClient.post<HttpRegistrationPostData>(
            this.registrationUrl, 
            JSON.stringify(registrationPostData), oprions)
            .subscribe(
                data => {
                if (data.toString().length > 0)
                {
                    console.log("success");
                    let obj = JSON.parse(data.toString());
                    if (obj.IsCorrect)
                    {
                        this.router.navigate(['/game']);
                    }
                    else 
                    {
                        this.router.navigate(['/login']);
                    }
                }
                },
                error => console.log(error)
            );
    }

    isAuthorized() : boolean
    {
        console.log('registration: onClick()');
        let oprions = {
            headers: new HttpHeaders({ 
            'Content-Type': 
                'application/json' })
        }
        this.httpClient.get<boolean>(
        "http://localhost:6430/api/auth/isauth")
        .subscribe(
            data => {
                console.log('success')
                return true;
            },
            error => {
                console.log("error: " + error)
                return false;
            }
        );
        return false;
    }

}
