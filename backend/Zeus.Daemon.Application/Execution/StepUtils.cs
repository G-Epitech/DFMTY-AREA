using System.Collections;
using System.Reflection;

using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Extensions.Type;
using Zeus.Daemon.Application.Utils;

namespace Zeus.Daemon.Application.Execution;

public static class StepUtils
{
    public static object? GetFromIntegrationsValue(ParameterInfo parameterInfo, IReadOnlyCollection<Integration> integrations)
    {
        var attributeInfo = parameterInfo.GetFromIntegrationsParameterInfo();

        return attributeInfo.IsCollection
            ? GetIntegrationsCollection(attributeInfo.Type, integrations)
            : GetIntegrationValue(parameterInfo, integrations);
    }

    private static Integration? GetIntegrationValue(ParameterInfo parameterInfo, IReadOnlyCollection<Integration> integrations)
    {
        var integration = integrations.FirstOrDefault(i => i.GetType().IsAssignableTo(parameterInfo.ParameterType));

        if (integration is null && !parameterInfo.IsNullable())
        {
            throw new InvalidOperationException($"Integration of type '{parameterInfo.ParameterType.Name}' not found");
        }
        return integration;
    }

    private static IList GetIntegrationsCollection(Type destType, IReadOnlyCollection<Integration> integrations)
    {
        var collectionType = destType.GetGenericArguments()[0];
        var collection = (IList?)Activator.CreateInstance(typeof(List<>).MakeGenericType(collectionType));

        if (collection is null)
        {
            throw new InvalidOperationException($"Cannot create collection of type '{destType.Name}'");
        }

        foreach (var integration in integrations)
        {
            if (integration.GetType().IsAssignableTo(collectionType))
            {
                collection.Add(integration);
            }
        }
        return collection;
    }
}
