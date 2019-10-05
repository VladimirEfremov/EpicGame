import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { MainComponent } from './main/main.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { GameComponent } from './game/game.component';
import { GameGuard } from './shared/GameGuard';
const appRoutes = [
    { path: '', component: MainComponent },
    { path: 'login', component: LoginComponent },
    { path: 'registration', component: RegistrationComponent },
    { path: 'game', component: GameComponent, canActivate: [GameGuard] },
    { path: '**', component: NotFoundPageComponent }
];
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            MainComponent,
            LoginComponent,
            RegistrationComponent,
            NotFoundPageComponent,
            GameComponent
        ],
        imports: [
            BrowserModule,
            RouterModule.forRoot(appRoutes),
            HttpClientModule,
            FormsModule
        ],
        providers: [
            GameGuard
        ],
        bootstrap: [
            AppComponent
        ]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map