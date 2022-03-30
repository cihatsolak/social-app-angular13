import { Component, OnInit } from '@angular/core';
import { User } from '../_models/User';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  users!: User[];
  userParams: any = {};
  public loading = false;
  constructor(
    private userService: UserService,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.loading = true;
    this.userService.getUsers(null, this.userParams).subscribe(
      (apiResponse) => {
        this.users = apiResponse.data;
        this.loading = false;
      },
      (error) => {
        this.alertifyService.error(error);
        this.loading = false;
      }
    );
  }
}
