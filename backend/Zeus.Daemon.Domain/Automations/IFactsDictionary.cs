namespace Zeus.Daemon.Domain.Automations;

public interface IFactsDictionary : IDictionary<string, Fact>;
public interface IReadOnlyFactsDictionary : IReadOnlyDictionary<string, Fact>;
