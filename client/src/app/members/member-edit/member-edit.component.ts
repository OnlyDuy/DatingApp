import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
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
  @ViewChild('editForm') editForm!: NgForm;
  member!: Member;
  user!: User | null;

  constructor(private accountService: AccountService, private memberService: MembersService, 
    private toastr: ToastrService) {
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

  updateMember() {
    console.log(this.member);
    this.toastr.success("Profile updated successfully");
    this.editForm.reset(this.member);
  }
}
