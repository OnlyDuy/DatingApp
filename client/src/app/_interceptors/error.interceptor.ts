import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateErors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    //push: Nối các phần tử mới vào cuối một mảng và trả về độ dài mới của mảng.
                    modalStateErors.push(error.error.errors[key]);
                  }
                }
                throw modalStateErors.flat();
              } else if (typeof(error.error) === 'object'){
                this.toastr.error(error.error, error.status);
              } else {
                this.toastr.error(error.error, error.status);
              }
              break;
            case 401:
              this.toastr.error(error.error, error.status);
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              // Ở đây dùng bộ định tuyến để chuyển đổi trạng thái
              const navigationExtras: NavigationExtras = {state: {error: error.error}}
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error('Something unexpected went wrong');
              break;
          }
        }
        // Nếu không bắt được lỗi thì sẽ trả lại lỗi cho bất kỳ thứ gì đang gọi yêu cầu HTTP trong lần đầu tiên
        return throwError(error);
      })
    )
  }
}
