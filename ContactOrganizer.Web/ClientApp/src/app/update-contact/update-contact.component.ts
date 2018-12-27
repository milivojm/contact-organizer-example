import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from "@angular/router";
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ChangeContactModel } from '../models/ChangeContactModel';
import { ContactsService } from '../contacts.service';
import { ValidationMessage } from '../models/ValidationMessage';
import { AppModalComponent } from '../app-modal/app-modal.component';
import { switchMap } from 'rxjs/operators';
import { ContactDetails } from '../models/ContactDetails';

@Component({
  selector: 'app-update-contact',
  templateUrl: './update-contact.component.html',
  styleUrls: ['./update-contact.component.css']
})
export class UpdateContactComponent implements OnInit {
  model: ChangeContactModel;
  errorList: ValidationMessage[];

  constructor(private service: ContactsService, private router: Router, private modalService: NgbModal, private activatedRoute: ActivatedRoute) {
    this.model = new ChangeContactModel();
    this.errorList = [];
  }

  ngOnInit() {
    this.activatedRoute.paramMap.pipe(
      switchMap((params: ParamMap) => this.service.getContact(params.get('id')))
    ).subscribe(
      contactToUpdate => {
        this.model = new ChangeContactModel();
        this.model.initFromContactDetails(contactToUpdate)
      },
      error => console.log(error)
    );
  }

  onSubmit() {
    this.service.updateContact(this.model).subscribe(
      _updatedContact => this.router.navigate(['searchContacts']),
      response => {
        this.errorList = response.error.map(e => new ValidationMessage(e.propertyName, e.errorMessage));
        const modal = this.modalService.open(AppModalComponent);
        modal.componentInstance.errorList = this.errorList;
      }
    );
  }
}
