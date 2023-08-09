import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  member!: Member;
  user!: User | null;

  constructor(private accountService: AccountService, private memberService: MembersService) {
    // đưa người dùng tra khỏi vùng có thể quan sát được
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    if (this.user?.username) {
      this.memberService.getMember(this.user.username).subscribe(member => {
        this.member = member;
      })
    }
  }
}
