import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  isHaveUserName(): boolean{
    if(localStorage.getItem('userName') === null)
      return false;

    return true;
  }

  getUserName(): string | null{
    return localStorage.getItem('userName');
  }
}
