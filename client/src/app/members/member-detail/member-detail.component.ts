import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { reduce } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Message } from 'src/app/_models/message';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit{
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  member: Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  activeTab: TabDirective;
  messages: Message[] = [];

  constructor(private memberService: MembersService, private route: ActivatedRoute, private router: Router,
    private messageService: MessageService) { }

  ngOnInit(): void {

    this.route.data.subscribe(data => {
      this.member = data['member'];
    })

    this.route.queryParams.subscribe(params => {
      params['tab'] ? this.selectTab(params['tab']) : this.selectTab(0);
    })

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false,
      }
    ]

    this.galleryImages = this.getImages();
  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.member.photos) {
      imageUrls.push({
        // ở đây có thể người dùng không có ảnh nên sẽ chỉ sd tham số chuỗi tùy chọn ( dấu ?)
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }

  loadMessages() {
    this.messageService.getMessageThread(this.member?.username).subscribe(message => {
      this.messages = message;
    })
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  // Tạo 1 phương thức đã kích hoạt để nhận dữ liệu
  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    // truy cập đúng tiêu đề tin nhắn
    if (this.activeTab.heading === 'Messages' && this.messages.length === 0) {
      this.loadMessages();
    }
  }
}
