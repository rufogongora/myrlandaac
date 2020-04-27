import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Account } from '../_models/account.model';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.css']
})
export class MyAccountComponent implements OnInit {

  account: Account;
  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.me()
    .subscribe(res => {
      this.account = res;
    })
  }

}
