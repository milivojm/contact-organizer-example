import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateContactModel } from './models/CreateContactModel';
import { ContactDetails } from './models/ContactDetails';
import { Observable } from 'rxjs';
import { ValidationMessage } from './models/ValidationMessage';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {
  constructor(private httpClient: HttpClient) { }

  getContacts(firstName?: string, lastName?: string, telephoneNuber?: string, fullAddress?: string, startFrom = 0, sortBy = "FirstName") {
    let queryUrl = `api/contacts?firstName=${firstName}&lastName=${lastName}&telephoneNumber=${telephoneNuber}&address=${fullAddress}&sortBy=${sortBy}&startFrom=${startFrom}`;
    return this.httpClient.get(queryUrl);    
  }

  createContact(model: CreateContactModel): Observable<ContactDetails | ValidationMessage> {
    let postUrl = 'api/contacts/create';
    return this.httpClient.post<ContactDetails>(postUrl, model);
  }
}
