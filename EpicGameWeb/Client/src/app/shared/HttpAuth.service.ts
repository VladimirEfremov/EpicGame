import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Md5 } from 'ts-md5/dist/md5';
import { AccountData } from './AccountData';
import { Observable } from 'rxjs';

import { HttpLoginPostData } from './HttpLoginPostData';
import { HttpRegistrationPostData } from './HttpRegistrationData';
import { UserInfo } from '../game/UserInfo';
import { TwoUsers } from '../game/TwoUsers';

@Injectable()
export class HttpAuthService
{
    loginUrl: string = 
        //"http://httpbin.org/post"; 
        "http://localhost:6430/Auth/Login";

    registrationUrl: string = 
        "http://localhost:6430/Auth/Registration";
    
    getAccountUrl: string = 
        "http://localhost:6430/Auth/GetAccountData";    

    addUserToFriendsUrl:string =
        "http://localhost:6430/Auth/AddUserToFriends";
    removeUserFromFriendsUrl:string =
        "http://localhost:6430/Auth/RemoveUserFromFriends";


    getAllUsersUrl:string =
        "http://localhost:6430/Auth/GetAllUsersInfo"; 

    getUsersFriendsUrl:string =
        "http://localhost:6430/Auth/GetUsersFriendsInfo"; 

    getUsersFollowersUrl:string =
        "http://localhost:6430/Auth/GetUsersFollowersInfo"; 

    getUsersFollowingsUrl:string =
        "http://localhost:6430/Auth/GetUsersFollowingsInfo"; 

    signOutUrl: string = 
        "http://localhost:6430/Auth/SignOut"; 

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
                (data) => {
                    if (data.toString() === "true")
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
                error => console.log("error: "+error)
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
                        this.router.navigate(['/game-menu']);
                        //this.router.navigate(['/registration']);
                    }
                }
                },
                error => console.log(error)
            );
    }

    GetAccountData() : Observable<AccountData>
    {
        console.log('GetAccountData:');
        return this.httpClient.get<AccountData>(this.getAccountUrl);
    }

    public AddUserToFriends(users:TwoUsers):void
    {
        this.httpClient.post<TwoUsers>(
            this.addUserToFriendsUrl, 
            users)
        .subscribe(
            data => console.log("success"),
            error => console.log("error"+error)
        );;
    }
    
    public RemoveUserFromFriends(users:TwoUsers):void
    {
        this.httpClient.post<TwoUsers>(this.removeUserFromFriendsUrl, users);
    }

    public GetAllUsers():Observable<UserInfo[]>
    {
        console.log("GetAllUserInfo");
        let result = this.httpClient.get<UserInfo[]>(
            this.getAllUsersUrl);
        result.forEach(element => {
            for (var i=0; i < element.length; i++)
            {
                console.log("user nick: "+element[i].Nickname);
            }
        });

        return result;
    }

    public GetUsersFriendsInfo(userId:number):Observable<UserInfo[]>
    {
        return this.httpClient
            .get<UserInfo[]>(
            this.getUsersFriendsUrl);
    }

    public GetUsersFollowersInfo(userId:number):Observable<UserInfo[]>
    {
        return this.httpClient
        .get<UserInfo[]>(
            this.getUsersFollowersUrl);
    }

    public GetUsersFollowingsInfo(userId:number):Observable<UserInfo[]>
    {
       return this.httpClient
        .get<UserInfo[]>(
        this.getUsersFollowingsUrl);
    }

    public SignOut() : void
    {
        //http post
        this.httpClient.post(this.signOutUrl, "");
    }


}
