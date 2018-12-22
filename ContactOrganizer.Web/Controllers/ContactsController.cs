using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactOrganizer.Infrastructure;
using ContactOrganizer.Web.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactOrganizer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactOrganizerRepository _repository;
        private readonly ContactOrganizerService _contactOrganizerService;

        public ContactsController(IContactOrganizerRepository repository)
        {
            _repository = repository;
            _contactOrganizerService = new ContactOrganizerService(_repository); // No DI here because ContactOrganizerService is core of the application!!!
        }

        [HttpGet()]
        public ActionResult<SearchContactsModel> SearchContacts(string firstName = "", string lastName = "", string telephoneNumber = "", string address = "", string sortBy = "", int startFrom = 0, int takeRows = 5)
        {
            List<Contact> contacts = _contactOrganizerService.FindContacts(firstName, lastName, telephoneNumber, address, startFrom, takeRows, sortBy, out int totalRows);

            return new SearchContactsModel()
            {                
                TotalRows = totalRows,
                Result = contacts
            };
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> GetContact(Guid id)
        {
            return _contactOrganizerService.GetContactById(id);
        }

        [HttpPost("create")]
        public ActionResult<Contact> CreateContact(ContactDetailsModel contactDetailsModel)
        {
            try
            {
                return _contactOrganizerService.CreateNewContact(contactDetailsModel);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
        }

        [HttpPost("update")]
        public ActionResult<Contact> UpdateContact(ContactDetailsModel contactDetailsModel)
        {
            try
            {
                return _contactOrganizerService.UpdateContactDetails(contactDetailsModel.Id, contactDetailsModel);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteContact(Guid id)
        {
            _contactOrganizerService.DeleteContact(id);
            return Ok();
        }
    }
}