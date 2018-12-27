import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChangeContactModel } from './models/ChangeContactModel';
import { ContactDetails } from './models/ContactDetails';
import { Observable } from 'rxjs';
import { ValidationMessage } from './models/ValidationMessage';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {
    
  constructor(private httpClient: HttpClient) { }

  getContact(id: string): Observable<ContactDetails> {
    let queryUrl = `api/contacts/${id}`;
    return this.httpClient.get<ContactDetails>(queryUrl);
  }

  getContacts(firstName?: string, lastName?: string, telephoneNuber?: string, fullAddress?: string, startFrom = 0, sortBy = "FirstName") {
    let queryUrl = `api/contacts?firstName=${firstName}&lastName=${lastName}&telephoneNumber=${telephoneNuber}&address=${fullAddress}&sortBy=${sortBy}&startFrom=${startFrom}`;
    return this.httpClient.get(queryUrl);    
  }

  createContact(model: ChangeContactModel): Observable<ContactDetails | ValidationMessage> {
    let postUrl = 'api/contacts/create';
    return this.httpClient.post<ContactDetails>(postUrl, model);
  }

  updateContact(model: ChangeContactModel): Observable<ContactDetails | ValidationMessage> {
    let postUrl = 'api/contacts/update';
    return this.httpClient.post<ContactDetails>(postUrl, model);
  }

  deleteContact(id: string) {
    let deleteUrl = `api/contacts/delete/${id}`;
    return this.httpClient.delete(deleteUrl);
  }
}
