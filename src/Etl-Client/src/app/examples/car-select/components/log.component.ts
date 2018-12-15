import * as signalR from '@aspnet/signalr';
import { OnInit, ChangeDetectionStrategy, Component } from '@angular/core';
@Component({
  selector: 'etl-client-log',
  templateUrl: './log.component.html'
})
export class LogComponent implements OnInit {
  private _hubConnection: signalR.HubConnection | undefined;
  messages: string[] = [];

  ngOnInit() {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:666/logger')
      .configureLogging(signalR.LogLevel.Information)
      .build();
    this._hubConnection.start().catch(err => console.error(err.toString()));
    this._hubConnection.on('Log', (data: any) => {
      const received = `${data}`;
      this.messages.push(received);
    });
  }
}
