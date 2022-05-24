namespace Application.Playground;

public class AdventurePlaygroundServiceException : Exception
{
    public AdventurePlaygroundServiceException(string? message)
        : base(message)
    {
    }
}