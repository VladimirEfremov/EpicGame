import { CanActivate, 
         ActivatedRouteSnapshot, 
         RouterStateSnapshot } 
    from '@angular/router';
import { Observable } 
    from 'rxjs'

export class GameGuard implements CanActivate
{
    constructor() 
    { }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot) 
        : Observable<boolean> | boolean
    {
        return confirm('Wanna go?');
    }
}
