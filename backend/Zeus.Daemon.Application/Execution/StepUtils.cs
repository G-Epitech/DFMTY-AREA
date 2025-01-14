using System.Collections;
using System.Reflection;

using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Extensions.Type;

namespace Zeus.Daemon.Application.Execution;

public static class StepUtils
{
    public static object? GetFromIntegrationsValue(ParameterInfo parameterInfo, IReadOnlyCollection<Integration> integrations)
    {
        var genericType = parameterInfo.ParameterType.IsGenericType ? parameterInfo.ParameterType.GetGenericTypeDefinition() : null;
        var genericArg = parameterInfo.ParameterType.GetGenericArguments().FirstOrDefault();
        var genericList = genericType is not null ? typeof(IList<>).MakeGenericType(genericArg!) : null;
        var genericReadOnlyList = genericType is not null ? typeof(IReadOnlyList<>).MakeGenericType(genericArg!) : null;
        var isGenericCollection = genericList is not null && genericReadOnlyList is not null &&
                                  (genericList.IsAssignableTo(parameterInfo.ParameterType) || genericReadOnlyList.IsAssignableTo(parameterInfo.ParameterType));

        return isGenericCollection
            ? GetIntegrationsCollection(parameterInfo.ParameterType, integrations)
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
