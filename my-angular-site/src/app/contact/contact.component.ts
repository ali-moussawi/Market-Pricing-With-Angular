import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { Router } from '@angular/router';
@Component({
  selector: 'app-contact',
  template: `
  <section class="hero is-info is-bold">

  <div class="hero-body">
    <div class="container">
      <h1 class="title">
        Feed Back !
      </h1>
    </div>
  </div>
  </section>
  <section class="section">
<div class="container">
 
  <!-- contact form-->
  <form (ngSubmit)="submitForm()">
    <!--we have two way to create form: template driven way(simple) and reactive way(according to api ,complex) -->
<div class="field">
  <label for="Name" class="Label" >Name</label>
  <input required type="text" name="name" id="Name" class="input"  [(ngModel)]="name">
</div>

<div class="field">
<label for="Email" class="Label">Email</label>
  <input required type="email" name="email" id="Email" class="input"  [(ngModel)]="email">
</div>
<div class="field">
  <label for="message"  >message</label>
  <input required placeholder="Enter Feedback here!" class="textarea"   name="message" id="message" class="input"  [(ngModel)]="message">
</div>
<button type="submit" class="button is-large is-primary">
  Send!
</button>

  </form>
</div>
  </section>

  `,
  styles: [
  ]
})
export class ContactComponent implements OnInit {
name;
email;
message;

constructor(private http: HttpClient,private router: Router){

}

  ngOnInit(): void {
  
}

submitForm(){


  if (this.name== null || this.email==null || this.message==null)
  {
    alert("Please Fill All fields");
  }
  else if(this.name!= null && this.email !=null && this.message!=null)
  {
    this.http.get('https://localhost:44370/api/feedback?name='+this.name+'&email='+this.email+'&message='+this.message)
    .subscribe((res)=>(

      this.router.navigateByUrl('Inbox')
    ))
  }

}
}



