import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: string = "https://localhost:7049/api/";
  
  // tạo 1 local để lưu người dùng hiện tại
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { } 

  login(model: any) {
    return this.http.post<User>(this.baseUrl + "account/login", model).pipe(
      // Xử lý lưu thông tin người dùng vào localStorage nếu đăng nhập thành công
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem("user", JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      }
      )
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }
 
  logout() {
    localStorage.removeItem("user");
    this.currentUserSource.next(null);
  }
}
