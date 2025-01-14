﻿namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class TriggerHandlerAttribute : Attribute
{
    public TriggerHandlerAttribute(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; }
}

