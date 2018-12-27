"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ChangeContactModel = /** @class */ (function () {
    function ChangeContactModel() {
    }
    ChangeContactModel.prototype.initFromContactDetails = function (contactDetails) {
        this.id = contactDetails.id;
        this.firstName = contactDetails.firstName;
        this.lastName = contactDetails.lastName;
        this.telephoneNumber = contactDetails.telephoneNumber;
        this.streetAndNumber = contactDetails.address.streetAndNumber;
        this.city = contactDetails.address.city;
        this.postalCode = contactDetails.address.postalCode;
        this.country = contactDetails.address.country;
    };
    return ChangeContactModel;
}());
exports.ChangeContactModel = ChangeContactModel;
//# sourceMappingURL=ChangeContactModel.js.map