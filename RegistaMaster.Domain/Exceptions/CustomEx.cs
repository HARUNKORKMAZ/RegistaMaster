namespace RegistaMaster.Domain.Exceptions
{
  public class CustomEx:Exception
  {
    public CustomEx( string message) :base(message)
    {
        
    }
    public CustomEx( string message , Exception innetException): base(message ,innetException)
    {
        
    }
      public override string ToString()
      {
        return Message;
    }
  }
}
