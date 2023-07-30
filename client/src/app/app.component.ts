import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.http.get('https://localhost:7049/api/users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error)
    });
  }
}
