using System.Collections.Generic;
using System.Threading.Tasks;
using X.Models;

namespace X.Models.Interfaces
{
  public interface IContactRepository
  {
    Task<List<Contact>> GetAllQueries();
    Task SubmitQuery(Contact contact);
  }
}