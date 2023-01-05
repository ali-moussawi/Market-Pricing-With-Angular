import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  template: `
   
  <section class="hero is-primary is-bold is-fullheight">
    <div class="hero-body">
      <div class="container has-text-centered">
        <h1 class="title">
          

        </h1>

      </div>

    </div>

  </section>


  `,
  styles: [
    `.hero{
      box-sizing: border-box;
      background-image:url('../../assets/img/bg.jpg') !important;
      background-size:cover;
      background-position:center center;
      background-color: rgba(255, 255, 255, 0.17);
      border-radius: 20px;
      box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
      backdrop-filter: blur(50px);
      -webkit-backdrop-filter: blur(5px);
      border: 1px solid rgba(255, 255, 255, 0.3);
    }
    `
  ]
})
export class HomeComponent {

}
