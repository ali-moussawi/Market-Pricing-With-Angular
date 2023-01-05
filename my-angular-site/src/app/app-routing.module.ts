import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import * as path from 'path';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { InboxComponent } from './inbox/inbox.component';

const routes: Routes = [
//here define our route
{
  path:'',
  component: HomeComponent
},
{
  path:'feedback',
  component:ContactComponent
}
,
{
  path:'Inbox',
  component:InboxComponent
}

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
