using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RegistaMaster.Application.Repositories;
using RegistaMaster.Application.Services.SecurityService;
using RegistaMaster.Domain.DTOModels.SecurityModels;
using RegistaMaster.Persistance.RegistaMasterContextes;
using RegistPackets.FileService.Interfaces;

namespace RegistaMaster.Infrastructure.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly RegistaMasterContext context;
    private readonly SessionModel session;
    private readonly IConfiguration config;
    private readonly IFileService fileService;
    public UnitOfWork(RegistaMasterContext _context, ISessionService sessionService, IConfiguration _config, IFileService _fileService)
    {
      session = sessionService.GetInjection();
      context = _context;
      config = _config;
      fileService = _fileService;
    }
    private IRepository _repository;
    public IRepository Repository
    {
      get => _repository ?? (_repository = new Repository(context, session));
    }
    private readonly IActionNoteRepository _actionNoteRepository;
    public IActionNoteRepository ActionNoteRepository
    {
      get => _actionNoteRepository ?? new ActionNoteRepository(context, session, this);
    }
    private readonly IHomeRepository _homeRepository;
    public IHomeRepository HomeRepository
    {
      get => _homeRepository ?? new HomeRepository(context, this, session);
    }

    private readonly ICustomerRepository _customerRepository;
    public ICustomerRepository CustomerRepository
    {
      get => _customerRepository ?? new CustomerRepository(context, session, this);
    }

    IActionRepository _actionRepository;
    public IActionRepository ActionRepository
    {
      get => _actionRepository ?? new ActionRepository(context, session, this);
    }

    public IUserRepository _userRepository;
    public IUserRepository UserRepository
    {
      get => _userRepository ?? new UserRepository(context, this, session);
    }

    public IRequestRepository _requestRepository;
    public IRequestRepository RequestRepository {
      get => _requestRepository ?? new RequestRepository(context, session ,this , config,fileService);
    }



    public async Task<int> SaveChanges()
    {
      try
      {
        return await context.SaveChangesAsync();
      }
      catch (Exception e)
      {

        throw e;
      }
    }
    public SessionModel GetSession()
    {
      try
      {
        return session;

      }
      catch (Exception e)
      {

        throw e;
      }
    }
  }
}
