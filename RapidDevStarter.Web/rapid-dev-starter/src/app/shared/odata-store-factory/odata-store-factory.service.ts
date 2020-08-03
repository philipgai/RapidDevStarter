import { Injectable } from '@angular/core';
import ODataStore from 'devextreme/data/odata/store';

export interface IComplexObject {
  objectName: string,
  keyProp: string,
  complexObject?: IComplexObject
}

export interface IODataStoreCreateOptions {
  url: string,
  key: string,
  getItems: () => any[],
  complexObjects: IComplexObject[],
  beforeSend?: (options: { url?: string; async?: boolean; method?: string; timeout?: number; params?: any; payload?: any; }) => any
}

@Injectable({
  providedIn: 'root'
})
export class ODataStoreFactoryService {
  constructor() { }

  public create = (createOptions: IODataStoreCreateOptions): ODataStore => {
    return new ODataStore({
      url: createOptions.url,
      key: createOptions.key,
      version: 4,
      beforeSend: (options) => {
        if (options.method === 'PATCH') {
          options.method = 'PUT';

          // Get the key from /endpoints({key})
          var regExp = /\(([^)]+)\)/;
          var matches = regExp.exec(options.url);
          var key: number = +matches[1];

          const items = createOptions.getItems();
          var original = items.find(item => item[createOptions.key] === key);
          createOptions.complexObjects.forEach(complexObj => this.updateComplexObject(original, complexObj, options.payload))

          const updated = { ...original, ...options.payload }
          options.payload = updated;
        }
        if (createOptions.beforeSend) createOptions.beforeSend(options);
      }
    })
  };

  private updateComplexObject = (original: any, complexObj: IComplexObject, payload?: any) => {
    if (complexObj.complexObject) {
      this.updateComplexObject(original[complexObj.objectName], complexObj.complexObject, payload[complexObj.objectName])
    }

    let updatedComplexObj = { ...original[complexObj.objectName], ...payload[complexObj.objectName] };
    let removeComplexObj = true;
    Object.keys(updatedComplexObj).forEach(key => {
      updatedComplexObj[key] = updatedComplexObj[key] || null;
      if (removeComplexObj && key !== complexObj.keyProp && updatedComplexObj[key]) {
        removeComplexObj = false;
      }
    });

    if (removeComplexObj)
      updatedComplexObj = null;

    payload[complexObj.objectName] = updatedComplexObj;
  }
}
