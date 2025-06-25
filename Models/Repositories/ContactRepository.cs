using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using X.Data;
using X.Models.Interfaces;

namespace X.Models.Repositories
{
  public class ContactRepository : IContactRepository
  {
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<Contact>> GetAllQueries()
    {
      return await _context.Contacts.OrderByDescending(c => c.AskedOn).ToListAsync();
    }

    public async Task SubmitQuery(Contact contact)
    {
      if (contact == null)
      {
        throw new ArgumentNullException(nameof(contact), "Contact cannot be null.");
      }

      _context.Contacts.Add(contact);
      await _context.SaveChangesAsync();

    }
  }
}
