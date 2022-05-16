import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  loggedIn: boolean;

  constructor(private accountSerivce:AccountService) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  login() {
    this.accountSerivce.login(this.model).subscribe(response => {
      console.log(response);
      this.loggedIn = true
    }, error => {
      console.log(error);
    })
    
  }

  logout() {
    this.accountSerivce.logout();
    this.loggedIn = false;
  }

  getCurrentUser() {
    this.accountSerivce.currentUser$.subscribe(user => {
      this.loggedIn = !!user;
    }, error => {
      console.log(error);
    })
  }

}
