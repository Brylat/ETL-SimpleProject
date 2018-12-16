import { Component, ChangeDetectorRef } from '@angular/core';
import { ChangeDetectionStrategy } from '@angular/core';
import { OnInit } from '@angular/core';
import { MatTableDataSource, MatSort } from '@angular/material';
import { ViewChild } from '@angular/core';
import { CarDataService } from './car-data.service';
import { CarAdData } from '../../shared/interfaces';
import { ElementRef } from '@angular/core';

@Component({
  selector: 'etl-client-car-table',
  templateUrl: './car-table.component.html',
  styleUrls: ['./car-table.component.scss']
})
export class CarTableComponent implements OnInit {
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('downloadCsv') private downloadCsv: ElementRef;

  displayedColumns: string[] = [
    'brand',
    'model',
    'offer',
    'price',
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
    'particleFilter',
    'articleUrl'
  ];
  dataSource;

  constructor(
    private _carDataService: CarDataService,
    private changeDetector: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this._carDataService.retreiveCarAdsData().subscribe(d => {
      this.dataSource = new MatTableDataSource(d);
      this.dataSource.sort = this.sort;
      this.changeDetector.detectChanges();
    });
  }

  public async downloadCarCsv(): Promise<void> {
    const blob = await this._carDataService.downloadSingleCsv();
    const url = window.URL.createObjectURL(blob);
    const link = this.downloadCsv.nativeElement;
    link.href = url;
    link.download = 'CarDataCsv.csv';
    link.click();

    window.URL.revokeObjectURL(url);
  }
}
