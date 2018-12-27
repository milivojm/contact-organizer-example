import { Component, OnInit, Input } from '@angular/core';
import { ContactDetails } from '../models/ContactDetails';
import { ContactsService } from '../contacts.service';
import { SearchContactsModel } from '../models/SearchContactsModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-contacts',
  templateUrl: './search-contacts.component.html',
  styleUrls: ['./search-contacts.component.css']
})
export class SearchContactsComponent implements OnInit {
  model: SearchContactsModel;
  firstNameFilter: string;
  lastNameFilter: string;
  telephoneNumberFilter: string;
  addressFilter: string;
  sortByColumn: string;
  startFrom: number;
  readonly pageSize = 5;

  constructor(private contactsService: ContactsService, private router: Router) {
    this.model = new SearchContactsModel();
    this.model.result = [];
    this.resetFilter();
  }

  private resetFilter() {
    this.firstNameFilter = '';
    this.lastNameFilter = '';
    this.telephoneNumberFilter = '';
    this.addressFilter = '';
    this.sortByColumn = 'FirstName';
    this.startFrom = 0;
  }

  private getContacts() {
    this.contactsService.getContacts(this.firstNameFilter, this.lastNameFilter, this.telephoneNumberFilter, this.addressFilter, this.startFrom, this.sortByColumn).subscribe((data: SearchContactsModel) => {
      this.model = new SearchContactsModel();
      this.model.startFrom = data.startFrom;
      this.model.totalRows = data.totalRows;

      this.model.result = data.result.map(d => {
        let contactDetails = new ContactDetails();
        contactDetails.id = d.id;
        contactDetails.firstName = d.firstName;
        contactDetails.lastName = d.lastName;
        contactDetails.fullAddress = d.fullAddress;
        contactDetails.telephoneNumber = d.telephoneNumber;
        return contactDetails;
      });
    });
  }

  ngOnInit() {
    this.getContacts();
  }

  previousPage() {
    if (this.startFrom > 0) {
      this.startFrom = this.startFrom - this.pageSize;
      this.getContacts();
    }
  }

  nextPage() {
    if (this.startFrom < this.model.totalRows - 5) {
      this.startFrom = this.startFrom + this.pageSize;
      this.getContacts();
    }
  }

  isPreviousDisabled() {
    return this.startFrom == 0;
  }

  isNextDisabled() {
    return this.startFrom >= this.model.totalRows - 5;
  }

  onSearch() {
    this.startFrom = 0;
    this.getContacts();
  }

  onResetFilter() {
    this.resetFilter();
    this.getContacts();
  }

  onDelete(id: string) {
    if (confirm('Are you sure you want to delete a contact?')) {
      this.contactsService.deleteContact(id).subscribe(
        () => { this.getContacts(); },
        errorResponse => console.log(errorResponse)
      );
    }
  }

  onUpdate(id: string) {
    this.router.navigate(['updateContact', id]);
  }
}
