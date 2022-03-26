import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserDetail } from 'src/app/_models/User';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css'],
})
export class MemberDetailsComponent implements OnInit {
  userDetail!: UserDetail;
  constructor(
    private userService: UserService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getUserById();
  }

  getUserById() {
    let userId: number = this.route.snapshot.params['id'];
    this.userService.getUserById(userId).subscribe(
      (apiResponse) => {
        this.userDetail = apiResponse.data;
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }
}
