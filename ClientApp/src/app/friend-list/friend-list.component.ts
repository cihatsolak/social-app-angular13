import { Component, OnInit } from '@angular/core';
import { User } from '../_models/User';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css'],
})
export class FriendListComponent implements OnInit {
  users!: User[];
  followParams: string = 'followings';

  constructor(
    private userService: UserService,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers(this.followParams).subscribe(
      (apiResponse) => {
        this.users = apiResponse.data;
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }
}
