import { NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { SharedModule } from '@app/shared';
import { environment } from '@env/environment';

import { FEATURE_NAME, reducers } from './examples.state';
import { ExamplesRoutingModule } from './examples-routing.module';
import { ExamplesComponent } from './examples/examples.component';
import { FormComponent } from './form/components/form.component';
import { FormEffects } from './form/form.effects';
import { NotificationsComponent } from './notifications/components/notifications.component';
import { ExamplesEffects } from './examples.effects';
import { CarSelectComponent } from './car-select/components/car-select.component';
import { CarSelectService } from './car-select/components/car-select.service';
import { LogComponent } from './car-select/components/log.component';
import { CarTableComponent } from './car-table/car-table.component';
import { CarDataService } from './car-table/car-data.service';

@NgModule({
  imports: [
    SharedModule,
    ExamplesRoutingModule,
    StoreModule.forFeature(FEATURE_NAME, reducers),
    TranslateModule.forChild({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      },
      isolate: true
    }),
    EffectsModule.forFeature([ExamplesEffects, FormEffects])
  ],
  declarations: [
    ExamplesComponent,
    FormComponent,
    NotificationsComponent,
    CarSelectComponent,
    LogComponent,
    CarTableComponent
  ],
  providers: [CarSelectService, CarDataService]
})
export class ExamplesModule {
  constructor() {}
}

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(
    http,
    `${environment.i18nPrefix}/assets/i18n/examples/`,
    '.json'
  );
}
