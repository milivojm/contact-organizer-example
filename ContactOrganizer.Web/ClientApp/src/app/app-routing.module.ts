import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SearchContactsComponent } from './search-contacts/search-contacts.component';
import { NewContactComponent } from './new-contact/new-contact.component';
import { UpdateContactComponent } from './update-contact/update-contact.component';

const routes: Routes = [
  { path: 'searchContacts', component: SearchContactsComponent },
  { path: 'newContact', component: NewContactComponent },
  { path: 'updateContact/:id', component: UpdateContactComponent },
  { path: '', redirectTo: 'searchContacts', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
