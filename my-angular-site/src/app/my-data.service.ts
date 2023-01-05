import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MyDataService {

  constructor(private http: HttpClient) {

  }
getMessages(){

  return this.http.get(
  'https://localhost:44370/api/feedback/1'
  
  )
  }
   
}
