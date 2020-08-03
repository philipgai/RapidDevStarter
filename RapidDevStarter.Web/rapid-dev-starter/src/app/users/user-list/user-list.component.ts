import { Component, OnInit } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import ODataStore from 'devextreme/data/odata/store';
import { environment } from '../../../environments/environment';
import { ODataStoreFactoryService } from 'src/app/shared/odata-store-factory/odata-store-factory.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  private readonly userApiUrl = `${environment.connectionStrings.rapidDevStarterApi}/Users`

  public readonly userDataSource: DataSource = new DataSource({
    store: this._odataStoreFactory.create({
      url: this.userApiUrl,
      key: 'UserKey',
      getItems: () => this.userDataSource.items(),
      complexObjects: [{ objectName: 'ContactInfo', keyProp: 'ContactInfoUserKey' }]
    }),
    expand: ['ContactInfo'],
  })

  constructor(private readonly _odataStoreFactory: ODataStoreFactoryService) {

  }

  ngOnInit(): void {
  }
}
