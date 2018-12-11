import { Component } from '@angular/core';
import { ChangeDetectionStrategy } from '@angular/core';
import { OnInit } from '@angular/core';
import { MatTableDataSource, MatSort } from '@angular/material';
import { ViewChild } from '@angular/core';
import { CarDataService } from './car-data.service';
import { CarAdData } from '../../shared/interfaces';

@Component({
  selector: 'etl-client-car-table',
  templateUrl: './car-table.component.html',
  styleUrls: ['./car-table.component.scss']
})
export class CarTableComponent implements OnInit {
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns: string[] = [
    'brand',
    'model',
    'offer',
    'category',
    'version',
    'productionYear',
    'mileage',
    'capacity',
    'fuel',
    'horsePower',
    'transmission',
    'drivingGear',
    'type',
    'doorsNumber',
    'seatsNumber',
    'colour',
    'isMetallic',
    'condition',
    'firstRegistration',
    'isRegisteredInPoland',
    'countryOfOrigin',
    'isFirstOwner',
    'noAccidents',
    'serviceHistory',
    'vin',
    'particleFilter'
  ];
  dataSource;

  constructor(private _carDataService: CarDataService) {}

  ngOnInit() {
    this._carDataService.retreiveCarAdsData().subscribe(d => {
      this.dataSource = new MatTableDataSource(d);
      this.dataSource.sort = this.sort;
    });
  }
}
