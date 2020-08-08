import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faRocket } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  faRocket = faRocket;

  constructor(
    private readonly _router: Router
  ) { }

  ngOnInit(): void {
  }
}
