import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResponseDto } from '../../dto/response-dto';
import { UserListDto } from '../../dto/user-list-dto';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {
  userList: UserListDto[] = [];
  constructor(private readonly activatedRoute: ActivatedRoute) {
  }
  ngOnInit() {
    const initialData: ResponseDto<UserListDto[]> = this.activatedRoute.snapshot.data['DataResolver'];
    this.userList = initialData.result;
  }
}
