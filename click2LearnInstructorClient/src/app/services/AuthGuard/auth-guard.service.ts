import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from '../authentiication/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  userLoggedIn: boolean = false;
  constructor(private authService: AuthenticationService, private router: Router) {
    this.authService.userLoggedIn.subscribe((x: boolean) => this.userLoggedIn = x);
  }

  canActivate(): boolean {
    const UserId = localStorage.getItem("userID") ? true: false;
    if (this.userLoggedIn || UserId) {
      return true;
    } else {
      this.router.navigate(['/login']); // Redirect to login page if not authenticated
      return false;
    }
  }
}
