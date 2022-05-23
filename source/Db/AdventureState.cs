namespace Application.Playground;

public enum AdventureState
{
    Impossible, // script is absent or empty
    NotStarted, // script is present but no steps logged
    Pending, // there are step logs but not all steps taken
    Finished // all script steps are taken
}