using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API_CRUD.Data;
using Web_API_CRUD.Models;

namespace Web_API_CRUD.Controllers
{
    [Authorize (Roles ="Admin")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactsController : Controller
    {
        private readonly ConnectAPIDBContext dbContext;

        public ContactsController(ConnectAPIDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet/*("GetAll")*/] 
        public IActionResult GetContacts()
        {
           return Ok( dbContext.Contacts.ToList());
        } 

        [HttpPost("Add")]
        public IActionResult AdddContact(AddContactRequest addContactRequest)
        {

            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,
                Email = addContactRequest.Email,
            };
            dbContext.Contacts.Add(contact);
            dbContext.SaveChanges();

            return Ok(contact);
        }


        [HttpPut]
        [Route("update/{id:Guid}")]
        public IActionResult UpdateContact([FromRoute] Guid id , UpdateContactRequest updateContactRequest )
        {

            var contact = dbContext.Contacts.Find(id);

            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;
                dbContext.SaveChanges();
                return Ok(contact);

            }
            return NotFound();
        }


        [HttpGet]
        [Route("GetById/{id:Guid}")]
        public ActionResult Getcontact([FromRoute] Guid id)
        {
            var contact = dbContext.Contacts.Find(id);


            if (contact == null)
            {
                    return NotFound();

            }
            return Ok();
        }


        [HttpDelete]
        [Route("Delete/{id:Guid}")]
        public ActionResult Deletecontact([FromRoute] Guid id)
        {
            var contact = dbContext.Contacts.Find(id);
            if (contact != null)
            {
              dbContext.Remove(contact);
                dbContext.SaveChanges();
                return Ok(contact);

            }

            return NotFound();
        }
    }
}
