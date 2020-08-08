import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {

  public isDrawerOpen = true;

  public sideMenuData = [
    { id: 1, text: 'Accounts' }
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
