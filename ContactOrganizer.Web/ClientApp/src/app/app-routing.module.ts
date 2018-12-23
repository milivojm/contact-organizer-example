import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SearchContactsComponent } from './search-contacts/search-contacts.component';
import { NewContactComponent } from './new-contact/new-contact.component';

const routes: Routes = [
  { path: 'searchContacts', component: SearchContactsComponent },
  { path: 'newContact', component: NewContactComponent },
  { path: '', redirectTo: 'searchContacts', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
