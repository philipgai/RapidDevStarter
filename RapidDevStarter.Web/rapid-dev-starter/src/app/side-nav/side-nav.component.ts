import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {

  public isDrawerOpen = false;

  public sideMenuData = [
    { id: 1, text: 'Users', icon: 'user', onClick: () => this._router.navigate(['/users']) }
  ]

  constructor(private readonly _router: Router) { }

  ngOnInit(): void {
  }

}
