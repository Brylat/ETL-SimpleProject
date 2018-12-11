import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

import { ROUTE_ANIMATIONS_ELEMENTS, NotificationService } from '@app/core';
import { CarSelectService } from './car-select.service';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { Store } from '@ngrx/store';
import { State } from '../../examples.state';
import { TranslateService } from '@ngx-translate/core';
import { Observable, of } from 'rxjs';
import { Form } from '../../form/form.model';
import {
  Brand,
  brands,
  Price,
  prices,
  FuelType,
  fuelList,
  Year,
  yearList,
  Distance,
  distList
} from './car-select.data';
import { debounceTime, switchMap, map, startWith } from 'rxjs/operators';
import * as signalR from '@aspnet/signalr';
import {
  Car,
  City,
  IUserResponse,
  OtomotoUrl
} from '../../../shared/interfaces';

@Component({
  selector: 'etl-client-car-select',
  templateUrl: './car-select.component.html',
  styleUrls: ['./car-select.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CarSelectComponent implements OnInit {
  myControl = new FormControl();
  options: string[] = ['One', 'Two', 'Three'];

  routeAnimationsElements = ROUTE_ANIMATIONS_ELEMENTS;
  brands: Brand[] = brands;
  prices: Price[] = prices;
  fuelList: FuelType[] = fuelList;
  yearList: Year[] = yearList;
  distList: Distance[] = distList;
  cars: Car[] = [];
  selectedCar: string;
  selectedModel: string;
  priceFrom: number;
  priceTo: number;
  disableSelect: boolean = true;
  names: any;
  cities: City[] = [];
  filteredUsers: Observable<IUserResponse>;

  form = this.fb.group({
    autosave: false,
    brand: ['', [Validators.required]],
    model: ['', [Validators.required]],
    priceFrom: [''],
    priceTo: [''],
    fuelList: [''],
    yearFrom: [''],
    yearTo: [''],
    city: [''],
    dist: [''],
    username: ['', [Validators.required]],
    password: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    description: [
      '',
      [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(1000)
      ]
    ],
    requestGift: [''],
    birthday: ['', [Validators.required]],
    rating: [0, Validators.required],

    autoCompleteControl: null
  });

  formValueChanges$: Observable<Form>;

  constructor(
    private _carSelectService: CarSelectService,
    private fb: FormBuilder,
    private store: Store<State>,
    private translate: TranslateService,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.githubAutoComplete$ = this.form.get('city').valueChanges.pipe(
      startWith(''),
      debounceTime(150),
      switchMap(value => {
        if (value !== '') {
          return this.lookup(value);
        } else {
          return of(null);
        }
      })
    );
  }

  onSelectChange(event: any) {
    this.selectedCar = '';
    this.retreiveCarModels(event);
    this.disableSelect = false;
  }

  retreiveCarModels(carBrand: string) {
    this._carSelectService.retreiveCarModels(carBrand).subscribe(d => {
      console.log(d);
      this.cars = d;
      this.disableSelect = false;
    });
  }

  onCityChange(event: any) {
    this.form.controls['dist'].setValue('0');
  }

  save() {
    var urlAddress: OtomotoUrl = new OtomotoUrl();
    urlAddress.value = 'https://www.otomoto.pl/osobowe/';

    this.generatePathVariableUrl('brand', urlAddress, '');
    this.generatePathVariableUrl('model', urlAddress, '');
    this.generatePathVariableUrl('yearFrom', urlAddress, 'od-');
    this.generatePathVariableUrl('city', urlAddress, '');
    this.generateQueryPriceFrom(urlAddress);
    this.generateQueryPriceTo(urlAddress);
    this.generateQueryFuelType(urlAddress);
    this.generateQueryYearTo(urlAddress);
    this.generateQueryDisc(urlAddress);

    console.log(urlAddress.value);
    this.startFullEtl(urlAddress.value);

    this.generateUrl();
  }

  public githubAutoComplete$: Observable<City> = null;

  lookup(value: string): Observable<City[]> {
    return this._carSelectService
      .retreiveSuggestedCities(value)
      .pipe(map(results => results));
  }

  generateUrl() {
    var urlAddress = 'https://www.otomoto.pl/osobowe/';
  }

  generatePathVariableUrl(
    pathVariable: string,
    urlAddress: OtomotoUrl,
    prefix: string
  ) {
    if (
      this.form.get(pathVariable).value != undefined &&
      this.form.get(pathVariable).value != ''
    ) {
      urlAddress.value =
        urlAddress.value + prefix + this.form.get(pathVariable).value + '/';
    }
    if (pathVariable == 'city') {
      urlAddress.value = urlAddress.value + '?';
    }
  }

  generateQueryPriceFrom(urlAddress: OtomotoUrl) {
    if (
      this.form.get('priceFrom').value != undefined &&
      this.form.get('priceFrom').value != ''
    ) {
      urlAddress.value =
        urlAddress.value +
        '&search%5Bfilter_float_price%3Afrom%5D=' +
        this.form.get('priceFrom').value;
    }
  }

  generateQueryPriceTo(urlAddress: OtomotoUrl) {
    if (
      this.form.get('priceTo').value != undefined &&
      this.form.get('priceTo').value != ''
    ) {
      urlAddress.value =
        urlAddress.value +
        '&search%5Bfilter_float_price%3Ato%5D=' +
        this.form.get('priceTo').value;
    }
  }

  generateQueryFuelType(urlAddress: OtomotoUrl) {
    if (
      this.form.get('fuelList').value != undefined &&
      this.form.get('fuelList').value != ''
    ) {
      this.form.get('fuelList').value.forEach((element, index) => {
        urlAddress.value =
          urlAddress.value +
          '&search%5Bfilter_enum_fuel_type%5D%5B' +
          index +
          '%5D=' +
          element;
      });
    }
  }

  generateQueryYearTo(urlAddress: OtomotoUrl) {
    if (
      this.form.get('yearTo').value != undefined &&
      this.form.get('yearTo').value != ''
    ) {
      urlAddress.value =
        urlAddress.value +
        '&search%5Bfilter_float_year%3Ato%5D=' +
        this.form.get('yearTo').value;
    }
  }

  generateQueryDisc(urlAddress: OtomotoUrl) {
    if (
      this.form.get('dist').value != undefined &&
      this.form.get('dist').value != ''
    ) {
      urlAddress.value =
        urlAddress.value + '&search%5Bdist%5D=' + this.form.get('dist').value;
    }
  }

  getSuggestedCities(city: string) {
    this._carSelectService.retreiveSuggestedCities('skaw').subscribe(d => {
      this.cities = d;
    });
  }

  startFullEtl(url: string) {
    this._carSelectService.startFullEtl(url).subscribe();
  }
}
