import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';


platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));

  // Ở đây là một trình duyệt nền tảng động chịu trách nhiệm cung cấp mã để khởi động
  // mô đun ứng dụng của chúng ta