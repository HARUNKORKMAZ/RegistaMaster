namespace RegistaMaster.Domain.Exceptions
{
  public class UnAuth: Exception
  {
    public UnAuth(string message): base(message)
    {
        
    }
    public UnAuth(string message, Exception innerException): base(message,innerException)
    {
        
    }
    public override string ToString()
    {
      return Message;
    }
  }
}
