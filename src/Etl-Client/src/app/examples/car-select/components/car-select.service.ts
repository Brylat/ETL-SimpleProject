import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Stock } from '../../stock-market/stock-market.model';
import {
  CarContainer,
  EtlCommand,
  Car,
  City
} from '../../../shared/interfaces';

const URL_API = 'http://localhost:666/api';

@Injectable()
export class CarSelectService {
  constructor(private httpClient: HttpClient) {}

  retreiveCarModels(carBrand: string): Observable<Car[]> {
    return this.httpClient
      .get(URL_API + '/carmodel/' + carBrand)
      .pipe(map((response: Response) => response.json())).source;
  }

  retreiveSuggestedCities(city: string): Observable<City[]> {
    return this.httpClient
      .get(URL_API + '/city/' + city)
      .pipe(map((response: Response) => response.json())).source;
  }

  retreiveCar(carBrand: string): Observable<CarContainer> {
    return this.httpClient.get(URL + carBrand).pipe(
      map((CarContainer: any) => ({
        cars: CarContainer.cars
      }))
    );
  }

  startFullEtl(url: string) {
    return this.httpClient.post(URL_API + '/etl/fullEtl', new EtlCommand(url));
  }
}
