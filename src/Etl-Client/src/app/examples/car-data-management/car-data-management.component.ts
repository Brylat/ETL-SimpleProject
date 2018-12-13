import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { CarDataManagementService } from './car-data-management.service';
import { CarSelectService } from '../car-select/components/car-select.service';

import { NotificationService } from '@app/core';

@Component({
  selector: 'etl-client-car-management',
  templateUrl: './car-data-management.component.html',
  styleUrls: ['./car-data-management.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CarDataManagementComponent implements OnInit {
  constructor(
    private _carDataManagementService: CarDataManagementService,
    private readonly notificationService: NotificationService
  ) {}

  ngOnInit(): void {}

  cleanTempFolder() {
    this._carDataManagementService.cleanTempFolder().subscribe(d => {
      this.notificationService.success('Temp folders cleaned');
    });
  }
  cleanDatabase() {
    this._carDataManagementService.cleanDatabase().subscribe(d => {
      this.notificationService.success('Database cleaned');
    });
  }
}
