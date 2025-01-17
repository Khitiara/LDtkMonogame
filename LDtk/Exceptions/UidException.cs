namespace LDtk.Exceptions;

/// <summary>
/// Generic Uid Exception
/// </summary>
[System.Serializable]
public class UidException : System.Exception
{
    /// <summary>
    /// Generic Uid Exception
    /// </summary>
    public UidException() { }
    /// <summary>
    /// Generic Uid Exception
    /// </summary>
    public UidException(string message) : base(message) { }
    /// <summary>
    /// Generic Uid Exception
    /// </summary>
    public UidException(string message, System.Exception inner) : base(message, inner) { }
}
