"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ContactDetails = /** @class */ (function () {
    function ContactDetails() {
    }
    ContactDetails.prototype.getOneLineAddress = function () {
        if (this.fullAddress == undefined) {
            return '';
        }
        else {
            var regex = new RegExp(/\\n/g);
            return this.fullAddress.replace(regex, ', ');
        }
    };
    return ContactDetails;
}());
exports.ContactDetails = ContactDetails;
//# sourceMappingURL=ContactDetails.js.map