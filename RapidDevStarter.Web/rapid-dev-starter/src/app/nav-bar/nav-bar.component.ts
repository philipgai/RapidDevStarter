import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  constructor(
    private readonly _router: Router
  ) { }

  ngOnInit(): void {
  }

  public userMenuOptions = {
    dataSource: [{
      routerLink: "/users",
      name: "Users",
    }],
    displayExpr: "name",
    showFirstSubmenuMode: {
      name: "onClick",
      delay: { show: 0, hide: 300 }
    },
    orientation: "horizontal",
    submenuDirection: "auto",
    hideSubmenuOnMouseLeave: "false",
    onItemClick: (data) => {
      let item = data.itemData;
      if (item.routerLink) {
        this._router.navigate([item.routerLink]);
      }
    }
  }
}
