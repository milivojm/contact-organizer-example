import { ContactAddress } from './ContactAddress';

export class ContactDetails {
  id: string;
  firstName: string;
  lastName: string;
  telephoneNumber: string;
  fullAddress: string;
  address: ContactAddress;

  constructor() {
    this.address = new ContactAddress();
  }

  getOneLineAddress() {
    if (this.fullAddress == undefined) {
      return '';
    } else {
      let regex = new RegExp(/\\n/g);
      return this.fullAddress.replace(regex, ', ');
    }
  }
}
