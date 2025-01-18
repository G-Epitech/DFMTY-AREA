using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Execution;

public sealed class ActionResult
{
    private ActionError _error = new() { Message = "No facts returned", Details = "The action did not return any facts" };

    private FactsDictionary? _facts;

    public bool IsError => _facts == null;
    public ActionError Error => _facts is null ? _error : throw new InvalidOperationException("The result is not an error");
    public FactsDictionary Facts => _facts ?? throw new InvalidOperationException("The result is an error");

    public static implicit operator ActionResult(ActionError error) => new() { _error = error };
    public static implicit operator ActionResult(FactsDictionary facts) => new() { _facts = facts };
    public static ActionResult From(FactsDictionary facts) => new() { _facts = facts };
    public static ActionResult From(ActionError error) => new() { _error = error };
}
