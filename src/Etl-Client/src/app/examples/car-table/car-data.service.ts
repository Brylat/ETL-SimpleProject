import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { CarAdData } from '../../shared/interfaces';

const URL_API = 'http://localhost:666/api';

@Injectable()
export class CarDataService {
  constructor(private httpClient: HttpClient) {}

  retreiveCarAdsData(): Observable<CarAdData[]> {
    return this.httpClient
      .get(URL_API + '/etl/getAllCars/')
      .pipe(map((response: Response) => response.json())).source;
  }

  public async downloadSingleCsv(): Promise<Blob> {
    const file = await this.httpClient
      .get<Blob>(URL_API + '/etl/downloadAsCsv/', {
        responseType: 'blob' as 'json'
      })
      .toPromise();
    return file;
  }
}
