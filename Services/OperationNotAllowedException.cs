public class OperationNotAllowedException : Exception
{
  // Constructor
  public OperationNotAllowedException(int statusCode, string message) => (StatusCode, Message) = (statusCode, message);

  // Status code of the exception
  public int StatusCode { get; }
  public override string Message { get; }
}