import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit{
  member!: Member; 

  constructor(private memberService: MembersService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const username = this.route.snapshot.paramMap.get('username');
    if (username) {
      this.loadMember(username);
    } else {
      // Xử lý khi không có giá trị username
      this.router.navigate(['/not-found']);
    }
  }

  loadMember(username: string) {
    this.memberService.getMember(username).subscribe(member => {
      this.member = member;
    })
  }
}
