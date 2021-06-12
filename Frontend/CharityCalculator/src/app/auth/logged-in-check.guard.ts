import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class LoggedInCheckGuard implements CanActivate {

  constructor(private auth: AuthService, private router: Router) { }

  // Prevents going to login page when already logged in
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (this.auth.user$.getValue()) {
        this.router.navigate(["/main"])
        return false;
    }
    return true;
  }
  
}
