import { ContactDetails } from './ContactDetails';

export class ChangeContactModel {
  id: string;
  firstName: string;
  lastName: string;
  telephoneNumber: string;
  streetAndNumber: string;
  city: string;
  postalCode: string;
  country: string;

  constructor() { }

  initFromContactDetails(contactDetails: ContactDetails) {
    this.id = contactDetails.id;
    this.firstName = contactDetails.firstName;
    this.lastName = contactDetails.lastName;
    this.telephoneNumber = contactDetails.telephoneNumber;
    this.streetAndNumber = contactDetails.address.streetAndNumber;
    this.city = contactDetails.address.city;
    this.postalCode = contactDetails.address.postalCode;
    this.country = contactDetails.address.country;
  }
}
