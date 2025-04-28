import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResponseDto } from '../../dto/response-dto';
import { UserListResponse } from '../../dto/response/user-list-response';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {
  userList: UserListResponse[] = [];
  constructor(private readonly activatedRoute: ActivatedRoute) {
  }
  ngOnInit() {
    const initialData: ResponseDto<UserListResponse[]> = this.activatedRoute.snapshot.data['DataResolver'];
    this.userList = initialData.response;
  }
}
