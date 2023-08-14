import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();

  model: any = {};
  registerForm!: FormGroup;
  

  constructor(private accountService: AccountService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.intitializeForm();
  }

  intitializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required,
        Validators.minLength(4), Validators.maxLength(8)]),
      confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')]),
    });
  }

  // matchValues(matchTo: string): ValidatorFn {
  //   return (control: AbstractControl) => {
  //     return control?.value === control?.parent?.controls[matchTo].value 
  //       ? null : {isMatching: true}
  //   }
  // }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      const matchingControl = control.parent?.get(matchTo);
      
      if (matchingControl && control.value === matchingControl.value) {
        return null; // Khớp nhau, không có lỗi
      } else {
        return { isMatching: true }; // Không khớp nhau, trả về lỗi
      }
    };
  }
  

  register() {
    console.log(this.registerForm.value);
    // this.accountService.register(this.model).subscribe(response => {
    //   console.log(response);
    //   this.cancel();
    // }, error => {
    //   console.log(error);
    //   this.toastr.error(error.error);
    // })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
