import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http"
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { EventType } from 'src/app/models/models';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  /**
   * Fetches the current tax rate as an Observable
   */
  getCurrentRate(): Observable<number>{
    return this.http.get<number>(`${environment.apiUrl}/donation/currentrate`)
  }

  /**
   * Fetches the event types as an Observable
   */
  getEventTypes(): Observable<EventType[]>{
    return this.http.get<EventType[]>(`${environment.apiUrl}/donation/events`)
  }

  /**
   * Calculates the deductible amount for the given amount and event type
   * @param amount Amount to calculate for
   * @param event Event type
   */
  getDeductibleAmount(amount: number, event: string): Observable<number>{
    return this.http.post<number>(`${environment.apiUrl}/donation/calculateamount`, {amount, type: event})
  }

  /**
   * Updates the current tax rate
   * @param amount New value
   */
  setCurrentRate(amount: number): Observable<number>{
    return this.http.put<number>(`${environment.apiUrl}/donation/update`, {amount})
  }

  /**
   * Calculates the optimal splitting to get a maximal deductible amount
   * @param amount Amount to calculate for
   * @param type Event type
   */
  getOptimalSplit(amount: number, type: string): Observable<number[]>{
    var params = new HttpParams().set("amount", `${amount}`).set("type", type)
    return this.http.get<number[]>(`${environment.apiUrl}/donation/optimal`, {params})
  }

}
