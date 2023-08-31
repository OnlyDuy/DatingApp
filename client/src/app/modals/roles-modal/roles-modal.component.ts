import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.css']
})

export class RolesModalComponent implements OnInit {

  title: string;
  list: any[] = [];
  closeBtnName: string;

  // các thuộc tính trên khi tạo phiên bản Modal này sẽ tham chiếu đến bsModalRef
  constructor (public bsModalRef: BsModalRef) {}

  ngOnInit(): void {
    
  }
  
}
