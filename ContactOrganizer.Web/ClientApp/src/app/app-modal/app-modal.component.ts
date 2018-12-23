import { Component, OnInit, Input } from '@angular/core';
import { ValidationMessage } from '../models/ValidationMessage';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal',
  templateUrl: './app-modal.component.html',
  styleUrls: ['./app-modal.component.css']
})
export class AppModalComponent implements OnInit {
  private _visible: boolean;

  constructor(public activeModal: NgbActiveModal) {
    this._visible = false;
  }

  @Input() errorList: ValidationMessage[];

  @Input()
  set visible(visible: boolean) {
    this._visible = visible;
  }

  get visible(): boolean {
    return this._visible;
  }

  ngOnInit() {
  }

}
