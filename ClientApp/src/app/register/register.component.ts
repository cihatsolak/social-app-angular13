import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  userForRegisterDto: any = {};
  constructor(
    private authService: AuthService,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit(): void {}

  register() {
    this.authService.register(this.userForRegisterDto).subscribe(
      (response) => {
        this.alertifyService.success('kullanıcı oluşturuldu');
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }
}
