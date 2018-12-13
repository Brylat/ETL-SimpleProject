import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

const URL_API = 'http://localhost:666/api';

@Injectable()
export class CarDataManagementService {
  constructor(private httpClient: HttpClient) {}

  cleanTempFolder() {
    return this.httpClient.get(URL_API + '/etl/cleanTmpFolders');
  }

  cleanDatabase() {
    return this.httpClient.get(URL_API + '/etl/cleanDatabase');
  }
}
