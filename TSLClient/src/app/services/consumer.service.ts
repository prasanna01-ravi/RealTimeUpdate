import { EventEmitter, Injectable, Output } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class ConsumerService {

  private hubConnection: signalR.HubConnection;
  private readonly configuration: any = environment;
  @Output() message: EventEmitter<any> = new EventEmitter();

  constructor(private httpClient: HttpClient) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Trace)
      .withUrl(this.configuration.ws.serverUrl)
      .withAutomaticReconnect()
      .build();
  }

  public startConnection() {
    this.hubConnection
      .start()
      .then(() => {
        console.log('connection started');
        this.registerSignalEvents();
      })
      .catch(err => console.error('Error while starting connection: ' + err))
  }

  private registerSignalEvents() {
    this.hubConnection.on(this.configuration.ws.msgSubject, (data) => {
      this.message.emit(data);
    });

    this.hubConnection.onclose(() => {
      
    });
    
  }
}
