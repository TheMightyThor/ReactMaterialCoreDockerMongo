import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  constructor(private userService: UserService) { }
  submitted = false;
  model = {};
  onSubmit() :void  { 
    this.submitted = true;
    this.userService.createUser(this.model).subscribe();
  }

  newUser(){

  }
  ngOnInit() {

  }

}
