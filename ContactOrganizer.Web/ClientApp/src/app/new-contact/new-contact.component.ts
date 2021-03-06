import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ChangeContactModel } from '../models/ChangeContactModel';
import { ContactsService } from '../contacts.service';
import { ValidationMessage } from '../models/ValidationMessage';
import { AppModalComponent } from '../app-modal/app-modal.component';

@Component({
  selector: 'app-new-contact',
  templateUrl: './new-contact.component.html',
  styleUrls: ['./new-contact.component.css']
})

export class NewContactComponent implements OnInit {
  model: ChangeContactModel;
  errorList: ValidationMessage[];

  constructor(private service: ContactsService, private router: Router, private modalService: NgbModal) {
    this.model = new ChangeContactModel();
    this.errorList = [];
  }

  ngOnInit() {
  }

  onSubmit() {
    this.service.createContact(this.model).subscribe(
      _newContact => this.router.navigate(['searchContacts']),
      response => {
        this.errorList = response.error.map(e => new ValidationMessage(e.propertyName, e.errorMessage));
        const modal = this.modalService.open(AppModalComponent);
        modal.componentInstance.errorList = this.errorList;
      }
    );
  }
}
