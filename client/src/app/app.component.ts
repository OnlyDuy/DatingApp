import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

// Thành phần của Angular 
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

// OnInit: Mốc vòng đời được gọi ngay sau khi Angular đã được khởi tạo tất cả các thuộc tính 
// ràng buộc dữ liệu của một lệnh
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;

  constructor(private http: HttpClient, private accountService: AccountService ) { }

  ngOnInit() {
    this.getUsers();
    this.setCurrentUser();
  }

    // tạo 1 local để lưu người dùng hiện tại
  setCurrentUser() {
    // lấy câc đối tượng người dùng từ bộ nhớ cục bộ
    const userString: string | null = localStorage.getItem('user');
    if (userString !== null) {
      const user: User = JSON.parse(userString);
      // Thiết lập đối tượng đã lấy trong tài khoản của mình
      this.accountService.setCurrentUser(user);
    }
  }

  getUsers() {
    this.http.get('https://localhost:7049/api/users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error)
    });
  }
}
