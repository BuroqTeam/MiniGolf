using System;

/// <summary>
/// Validator fourth video. New Attribute.
/// </summary>
public class NeedsComponentAttribute : Attribute
{
    public Type type;

    public NeedsComponentAttribute(Type type)
    {
        this.type = type;
    }

    
}
