namespace Explore.Components;

public class Health
{
    public int Value;
    public int Max;

    public Health()
    {
    }
    
    public Health(int value)
    {
        Max = value;
        Value = Max;
    }
}