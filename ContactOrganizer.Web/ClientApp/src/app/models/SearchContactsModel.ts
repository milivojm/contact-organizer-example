import { ContactDetails } from './ContactDetails';

export class SearchContactsModel {
  totalRows: number;
  startFrom: number;
  result: ContactDetails[];
}
