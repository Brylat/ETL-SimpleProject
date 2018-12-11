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
}
