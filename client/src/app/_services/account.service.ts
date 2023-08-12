import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: string = environment.apiUrl;
  
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
          this.setCurrentUser(user);
        }
        return user;
      }
      )
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + "account/register", model).pipe(
      map((user: User) => {
        if (user) {
          // Lấy lại người dùng hiện tại từ API sẽ bao gồm cả ảnh của người dùng
          this.setCurrentUser(user);
        }
        // return user;
      })
    )
  }

  // Thiết lập người dùng hiện tại
  setCurrentUser(user: User) {
    localStorage.setItem("user", JSON.stringify(user));
    this.currentUserSource.next(user);
  }
 
  logout() {
    localStorage.removeItem("user");
    this.currentUserSource.next(null);
  }
}
