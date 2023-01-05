import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { MyDataService } from '../my-data.service';
@Component({
  selector: 'app-inbox',
  template: `


     <div class="table-container" style="margin:10px 0px 200px;background-color:black ">
  <table class="table table is-bordered is-striped is-narrow is-hoverable is-fullwidth">
  <thead style="background-color: blue; font-color:white ">
          <tr>
            <td>Name</td>
            <td>Gmail </td>
            <td>Message</td>
          </tr>
         
        </thead>
          <tbody>
          <tr *ngFor="let m of mydata">
        
        <td  >{{ m.name }}</td>
        <td>{{ m.email }}</td>
        <td>{{ m.message1 }}</td>
      </tr>



          </tbody>

  </table>
</div>
    
  `,
  styles: [`
 
  
  `
  ]
})
export class InboxComponent implements OnInit{
mydata:any;
constructor(private mydataservice : MyDataService){}

ngOnInit():void{

this.mydataservice.getMessages()
.pipe(take(1))
.subscribe((data)=>
(
this.mydata= data
)
)

}

}
