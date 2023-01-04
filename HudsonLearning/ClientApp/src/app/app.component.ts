import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'Hudson Learning App';
  users: any;
  constructor(private accountService: AccountService) {
  }
  ngOnInit() {
    this.setCurrentUser();
  }
  setCurrentUser() {
    const localUser = localStorage.getItem("user");
    if (localUser != null) {
      const user: User = JSON.parse(localUser);
      this.accountService.setCurrentUser(user);
    }
  }
}
