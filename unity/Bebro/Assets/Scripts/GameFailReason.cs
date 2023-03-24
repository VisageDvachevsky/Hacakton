public abstract class GameFailReason { }
public class InvalidAction : GameFailReason { }
public class RoverBrokenDown : GameFailReason {
    public readonly Rover.Rover.BreakDownCause BreakDownCase;

    public RoverBrokenDown(Rover.Rover.BreakDownCause breakDownCase)
    {
        BreakDownCase = breakDownCase;
    }
}
public class SampleBroken : GameFailReason {}
