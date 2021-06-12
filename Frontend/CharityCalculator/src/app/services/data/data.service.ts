import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http"
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { EventType } from 'src/app/models';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getCurrentRate(): Observable<number>{
    return this.http.get<number>(`${environment.apiUrl}/donation/currentrate`)
  }

  getEventTypes(): Observable<EventType[]>{
    return this.http.get<EventType[]>(`${environment.apiUrl}/donation/events`)
  }

}
