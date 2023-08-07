import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getHttpOptions() {
    const userToken = localStorage.getItem('user');
    if (userToken !== null) {
      return {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + JSON.parse(userToken)?.token
        })
      };
    } else {
      // Xử lý trường hợp không có thông tin user trong localStorage.
      return {};
    }
  }

  getMembers() {
    const httpOptions = this.getHttpOptions();
    return this.http.get<Member[]>(this.baseUrl + 'users', httpOptions);
  }

  getMember(username: string) {
    const httpOptions = this.getHttpOptions();
    return this.http.get<Member>(this.baseUrl + 'users/' + username, httpOptions);
  }
}
