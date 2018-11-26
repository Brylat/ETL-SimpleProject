import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Stock } from '../../stock-market/stock-market.model';

export interface Car {
  modelName: string;
  modelValue: string;
}

export interface CarContainer {
  cars: Car[];
}

export interface City {
  id: string;
  name: string;
}

export interface IUserResponse {
  results: City[];
}

export class City {
  constructor(public id: string, public name: string) { }
}

const URL = 'http://localhost:666/api/carmodel/';
const URL_Cities = 'http://localhost:8080/api/city/';


@Injectable()
export class CarSelectService {
  constructor(private httpClient: HttpClient) { }


  retreiveCarModels(carBrand: string): Observable<Car[]> {
    return this.httpClient
      .get(URL + carBrand).pipe(
        map((response: Response) => response.json())
      ).source;
  }

  retreiveSuggestedCities(city: string): Observable<City[]> {
    return this.httpClient
      .get(URL_Cities + city).pipe(
        map((response: Response) => response.json())
      ).source;
  }

  retreiveCar(carBrand: string): Observable<CarContainer> {
    return this.httpClient
      .get(URL + carBrand)
      .pipe(
        map((CarContainer: any) => ({
          cars: CarContainer.cars
        }))
      )
    /* map((carContainer: any) => ({
         cars: carContainer.cars
     }))*/

  }
  /* 
     retrieveStock(symbol: string): Observable<Stock> {
       return this.httpClient
         .get(PROXY_URL + `https://api.iextrading.com/1.0/stock/${symbol}/quote`)
         .pipe(
           map((stock: any) => ({
             symbol: stock.symbol,
             exchange: stock.primaryExchange,
             last: stock.latestPrice,
             ccy: 'USD',
             change: stock.close,
             changePositive: stock.change.toString().indexOf('+') === 0,
             changeNegative: stock.change.toString().indexOf('-') === 0,
             changePercent: stock.changePercent.toFixed(2)
           }))
         );
     } */
}