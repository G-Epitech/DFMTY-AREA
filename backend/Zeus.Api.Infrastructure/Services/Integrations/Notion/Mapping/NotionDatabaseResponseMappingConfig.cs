using Mapster;

using Zeus.Api.Domain.Integrations.Notion;
using Zeus.Api.Domain.Integrations.Notion.ValueObjects;
using Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts;
using Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts.Utils;

namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Mapping;

public class NotionDatabaseResponseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetNotionDatabaseResponse, NotionDatabase>()
            .Map(dest => dest.Id, src => new NotionDatabaseId(src.Id))
            .Map(dest => dest.Title, src => GetNotionTextString(src.Title, "No title"))
            .Map(dest => dest.Description,
                src => GetNotionTextString(src.Description, "No description"))
            .Map(dest => dest.Icon, src => GetNotionIconString(src.Icon))
            .Map(dest => dest.CreatedAt, src => DateTime.Parse(src.CreatedTime))
            .Map(dest => dest.CreatedBy, src => new NotionUserId(src.CreatedBy.Id))
            .Map(dest => dest.LastEditedAt, src => DateTime.Parse(src.LastEditedTime))
            .Map(dest => dest.LastEditedBy, src => new NotionUserId(src.LastEditedBy.Id))
            .Map(dest => dest.IsInline, src => src.IsInline)
            .Map(dest => dest.Parent, src => GetNotionParent(src.Parent))
            .Map(dest => dest.Uri, src => new Uri(src.Url))
            .Map(dest => dest.Archived, src => src.Archived)
            .Map(dest => dest.InTrash, src => src.InTrash);
    }

    private string GetNotionTextString(List<NotionTextContract> text, string defaultValue)
    {
        return text.FirstOrDefault()?.Text.Content ?? defaultValue;
    }

    private string? GetNotionIconString(NotionIconContract? iconContract)
    {
        if (iconContract is null)
        {
            return null;
        }

        return iconContract.Type switch
        {
            "external" => iconContract.External?.Url,
            "emoji" => iconContract.Emoji,
            _ => null
        };
    }

    private NotionParent GetNotionParent(NotionParentContract parent)
    {
        return parent.Type switch
        {
            "page_id" => new NotionParentPage(new NotionPageId(parent.PageId!)),
            "database_id" => new NotionParentDatabase(new NotionDatabaseId(parent.DatabaseId!)),
            "workspace" => new NotionParentWorkspace(),
            _ => throw new ArgumentOutOfRangeException(nameof(parent.Type))
        };
    }
}
