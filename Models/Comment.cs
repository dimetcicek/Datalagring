namespace Datalagring.Models;

using System;

public class Comment
{
    public string Message { get; set; } = null!;

    public DateTime TimeStamp { get; set; }
}