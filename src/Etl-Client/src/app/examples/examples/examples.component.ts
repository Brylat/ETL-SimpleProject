import { Store, select } from '@ngrx/store';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { routeAnimations, selectAuth } from '@app/core';
import { State as BaseSettingsState } from '@app/settings';

import { State as BaseExamplesState } from '../examples.state';

interface State extends BaseSettingsState, BaseExamplesState {}

@Component({
  selector: 'etl-client-examples',
  templateUrl: './examples.component.html',
  styleUrls: ['./examples.component.scss'],
  animations: [routeAnimations],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ExamplesComponent implements OnInit {
  isAuthenticated$: Observable<boolean>;

  examples = [
    { link: 'todos', label: 'etl-client.examples.menu.todos' },
    { link: 'carselect', label: 'etl-client.examples.menu.carselect' },
    { link: 'cartable', label: 'etl-client.examples.menu.cartable' },
    { link: 'theming', label: 'etl-client.examples.menu.theming' },
    { link: 'crud', label: 'etl-client.examples.menu.crud' },
    { link: 'form', label: 'etl-client.examples.menu.form' },
    { link: 'notifications', label: 'etl-client.examples.menu.notifications' },
    {
      link: 'authenticated',
      label: 'etl-client.examples.menu.auth',
      auth: true
    }
  ];

  constructor(private store: Store<State>) {}

  ngOnInit(): void {
    this.isAuthenticated$ = this.store.pipe(
      select(selectAuth),
      map(auth => auth.isAuthenticated)
    );
  }
}
