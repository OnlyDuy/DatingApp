import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { PresenceService } from './_services/presence.service';

// Đây là phần lộ trình ứng dụng của chúng ta

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

  constructor(private accountService: AccountService, private presence: PresenceService ) { }

  ngOnInit() {
    this.setCurrentUser();
  }

    // tạo 1 local để lưu người dùng hiện tại
  setCurrentUser() {
    // lấy câc đối tượng người dùng từ bộ nhớ cục bộ
    const userString: string | null = localStorage.getItem('user');
    if (userString !== null) {
      const user: User = JSON.parse(userString);
      // kiểm tra thực sự có người dùng hay không
      // để lấy vào SignR
      if (user) {
        // Thiết lập đối tượng đã lấy trong tài khoản của mình
        this.accountService.setCurrentUser(user);
        // tạo kết nối trung tâm và truyền đối tượng user
        // để khi tạo nó, có thể có quyền truy cập vào mã thông báo jwt của người dùng
        this.presence.createHubConnection(user);
      }
    }
  }
}
