import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
   <!--header -->
  <app-header></app-header>

   <!-- routes get injected here-->
   
    <router-outlet></router-outlet>

   <!-- Footer-->
<app-footer></app-footer>


  `,
  styles: []
})
export class AppComponent {
  title = 'my-angular-site';
}
