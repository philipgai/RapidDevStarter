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
    // TODO - Move ODataStore to factory service
    store: new ODataStore({
      url: this.userApiUrl,
      key: 'UserKey',
      version: 4,
      beforeSend: (options) => {
        if (options.method === 'PATCH') {
          options.method = 'PUT';

          var regExp = /\(([^)]+)\)/;
          var matches = regExp.exec(options.url);
          var userKey: number = +matches[1];

          const items = this.userDataSource.items();
          var original = items.find(item => item['UserKey'] === userKey);
          if (!original) {
            // TODO - Handle error
          }

          let updatedContactInfo = { ...original.ContactInfo, ...options.payload.ContactInfo };
          let removeContactInfo = true;
          Object.keys(updatedContactInfo).forEach(key => {
            updatedContactInfo[key] = updatedContactInfo[key] || null;
            if (removeContactInfo && key !== 'ContactInfoUserKey' && updatedContactInfo[key]) {
              removeContactInfo = false;
            }
          });

          if (removeContactInfo) updatedContactInfo = null;

          options.payload.ContactInfo = updatedContactInfo;

          const updated = { ...original, ...options.payload }
          options.payload = updated;
        }
      }
    }),
    expand: ['ContactInfo'],
  })

  constructor() { }

  ngOnInit(): void {
  }
}
