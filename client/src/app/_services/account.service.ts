import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: string = "https://localhost:7049/api/";

  constructor(private http: HttpClient) { } 

  login(model: any) {
    return this.http.post(this.baseUrl + "account/login", model);
  }
}
