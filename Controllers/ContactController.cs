using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using X.Hubs;
using X.Models;
using X.Models.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace X.Controllers
{
    public class ContactController : Controller
    {
        // private readonly IHubContext _hubContext;
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            // _hubContext = hubContext;
            _contactRepository = contactRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactInfo(string firstName, string lastName, string email, string phoneNumber, string message)
        {
            var contact = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phoneNumber,
                Message = message,
                AskedOn = DateTime.UtcNow
            };

            try
            {
                await _contactRepository.SubmitQuery(contact);
                return Ok(new { success = true, message = "✅ Submitted successfully." });
            }
            catch (System.Exception)
            {
                return Ok(new { success = false, message = "❌ Could not be submitted." });
            }
        }

    }
}
