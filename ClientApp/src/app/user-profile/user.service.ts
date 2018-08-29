import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class UserService {
  API_URL  =  'https://localhost:5001';
  constructor(private httpClient: HttpClient) { }

  createUser(data) : Observable<Object>  {
    return this.httpClient.post(`${this.API_URL}/api/User`,data, httpOptions).pipe(
      tap((data : String) => console.debug('done')), catchError(data));
  }
}
