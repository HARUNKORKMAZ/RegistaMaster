using RegistaMaster.Application.Repositories;
using RegistaMaster.Domain.DTOModels.Entities.CustomerModels;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Domain.Entities;
using RegistaMaster.Domain.Enums;
using RegistaMaster.Persistance.RegistaMasterContextes;
using System.Net.Sockets;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class CustomerRepository : Repository, ICustomerRepository
  {
    private readonly RegistaMasterContext context;
    private readonly IUnitOfWork unitOfWork;
    private readonly SessionModel session;
    public CustomerRepository(RegistaMasterContext _context, SessionModel _session, IUnitOfWork _unitOfWork) : base(_context, _session)
    {
      context = _context;
      unitOfWork = _unitOfWork;
      session = _session;
    }
    public async Task<string> CustomerAdd(Customer customer)
    {
      try
      {
        await unitOfWork.Repository.Add(customer);
        await unitOfWork.SaveChanges();
        return "1";
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public void Delete(int id)
    {
      var customer = GetNonDeletedAndActive<Customer>(t => t.Id == id);
      DeleteRange(customer.ToList());
      Delete<Customer>(id);
    }

    public async Task<IQueryable<CustomerDTO>> GetList()
    {
      try
      {
        return GetNonDeletedAndActive<Customer>(t => t.ObjectStatus == ObjectStatus.NonDeleted).Select(s => new CustomerDTO()
        {
          Id = s.Id,
          Name = s.Name,
          Email = s.Email,
          Address = s.Address
        });
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public async Task<string> Update(Customer customer)
    {
      Update(customer);
      await unitOfWork.SaveChanges();
      return "1";
    }
  }
}
