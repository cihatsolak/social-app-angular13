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
  constructor(
    private userService: UserService,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers().subscribe(
      (apiResponse) => {
        this.users = apiResponse.data;
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }
}
