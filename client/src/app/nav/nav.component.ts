import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  currentUser$: Observable<User>;

  constructor(private accountSerivce:AccountService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountSerivce.currentUser$;
  }

  login() {
    this.accountSerivce.login(this.model).subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
    
  }

  logout() {
    this.accountSerivce.logout();
  }



}
