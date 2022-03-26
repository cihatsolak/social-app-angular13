import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  userForLoginDto: any = {};

  constructor(
    public authService: AuthService,
    private router: Router,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit(): void {}

  login() {
    this.authService.login(this.userForLoginDto).subscribe(
      () => {
        this.alertifyService.success('login success');
        this.router.navigate(['/members']);
      },
      () => {
        this.alertifyService.error('Login failed');
      }
    );
  }

  loggedIn() {
    return this.authService.logginIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertifyService.warning('logout');
    this.router.navigate(['/home']);
  }
}
