import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuardService } from '@app/core';

import { ExamplesComponent } from './examples/examples.component';
import { ParentComponent } from './theming/parent/parent.component';
import { TodosContainerComponent } from './todos/components/todos-container.component';
import { FormComponent } from './form/components/form.component';
import { NotificationsComponent } from './notifications/components/notifications.component';
import { CarSelectComponent } from './car-select/components/car-select.component';
import { CarTableComponent } from './car-table/car-table.component';
import { CarDataManagementComponent } from './car-data-management/car-data-management.component';

const routes: Routes = [
  {
    path: '',
    component: ExamplesComponent,
    children: [
      {
        path: '',
        redirectTo: 'carselect',
        pathMatch: 'full'
      },
      {
        path: 'carselect',
        component: CarSelectComponent,
        data: { title: 'etl-client.examples.menu.carselect' }
      },
      {
        path: 'cartable',
        component: CarTableComponent,
        data: { title: 'etl-client.examples.menu.cartablee' }
      },
      {
        path: 'cardatamanagement',
        component: CarDataManagementComponent,
        data: { title: 'etl-client.examples.menu.cardatamanagement' }
      },
      {
        path: 'crud',
        redirectTo: 'crud/',
        pathMatch: 'full'
      },
      {
        path: 'form',
        component: FormComponent,
        data: { title: 'etl-client.examples.menu.form' }
      },
      {
        path: 'notifications',
        component: NotificationsComponent,
        data: { title: 'etl-client.examples.menu.notifications' }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExamplesRoutingModule {}
