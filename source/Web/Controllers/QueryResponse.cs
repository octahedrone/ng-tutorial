namespace Web.Controllers;

public class QueryResponse<TResult>
{
    public TResult Result { get; set; }
    public string ErrorMessage { get; set; }
    
    public bool Success => ErrorMessage == null;
}