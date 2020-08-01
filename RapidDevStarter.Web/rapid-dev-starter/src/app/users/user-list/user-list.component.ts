import { Component, OnInit } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import ODataStore from 'devextreme/data/odata/store';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  private readonly userApiUrl = `${environment.connectionStrings.rapidDevStarterApi}/Users`

  public readonly userDataSource: DataSource = new DataSource({
      store: new ODataStore({ url: this.userApiUrl, key: 'UserKey', version: 4 })
  })

  constructor() { }

  ngOnInit(): void {

  }
}
