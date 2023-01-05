import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  template: `
    <!-- header-->
    <div  class="navbar is-black">
    <!--logo -->
    <div class="navbar-brand">
    <a class="navbar-item" style="width: 200px; height:100px">
    
      <img src="./assets/img/icon.png" style="width:100px; height:100px" alt="">
    
    </a>


    </div>
<!-- -->
<div class="navbar-menu">
  <div class="navbar-end">
  <a href="https://localhost:44370/" class="navbar-item">Back To Application</a>
    <a routerLink="/" class="navbar-item">Home</a>
    <a routerLink="/feedback" class="navbar-item">Feedback</a>
    <a routerLink="/Inbox" class="navbar-item">ALL Feedbacks</a>
    

  </div>
</div>

   </div>



  `,
  styles: [
  ]
})
export class HeaderComponent {

}
