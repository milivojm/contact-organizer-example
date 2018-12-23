import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {
  constructor(private httpClient: HttpClient) { }

  getContacts(firstName?: string, lastName?: string, telephoneNuber?: string, fullAddress?: string, startFrom = 0, sortBy = "FirstName") {
    let queryUrl = `api/contacts?firstName=${firstName}&lastName=${lastName}&telephoneNumber=${telephoneNuber}&address=${fullAddress}&sortBy=${sortBy}&startFrom=${startFrom}`;
    return this.httpClient.get(queryUrl);    
  }
}
