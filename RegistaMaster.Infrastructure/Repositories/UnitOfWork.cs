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

    private  IActionNoteRepository _actionNoteRepository;
    public IActionNoteRepository ActionNoteRepository
    {
      get => _actionNoteRepository ?? (_actionNoteRepository = new ActionNoteRepository(context, session, this));
    }

    private  IHomeRepository _homeRepository;
    public IHomeRepository HomeRepository
    {
      get => _homeRepository ?? (_homeRepository = new HomeRepository(context, this, session));
    }

    private  ICustomerRepository _customerRepository;
    public ICustomerRepository CustomerRepository
    {
      get => _customerRepository ??(_customerRepository =  new CustomerRepository(context, session, this));
    }

    private IActionRepository _actionRepository;
    public IActionRepository ActionRepository
    {
      get => _actionRepository ??(_actionRepository =  new ActionRepository(context, session, this));
    }

    private IUserRepository _userRepository;
    public IUserRepository UserRepository
    {
      get => _userRepository ?? (_userRepository = new UserRepository(context, this, session));
    }

    private IRequestRepository _requestRepository;
    public IRequestRepository RequestRepository
    {
      get => _requestRepository ??(_requestRepository=  new RequestRepository(context, session, this, config, fileService));
    }

    private IProjectRepository _projectRepository;
    public IProjectRepository ProjectRepository
    {
      get => _projectRepository ?? (_projectRepository = new ProjectRepository(this, context, session));
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
