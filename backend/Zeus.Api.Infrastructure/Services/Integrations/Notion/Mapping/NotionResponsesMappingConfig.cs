using Mapster;

using Zeus.Api.Domain.Integrations.Notion;
using Zeus.Api.Domain.Integrations.Notion.ValueObjects;
using Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts;
using Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts.Utils;

namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Mapping;

public class NotionResponsesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetNotionDatabaseResponse, NotionDatabase>()
            .MapWith(dest => new NotionDatabase(
                new NotionDatabaseId(dest.Id),
                GetNotionIconString(dest.Icon),
                DateTime.Parse(dest.CreatedTime),
                new NotionUserId(dest.CreatedBy.Id),
                DateTime.Parse(dest.LastEditedTime),
                new NotionUserId(dest.LastEditedBy.Id),
                GetNotionTextString(dest.Title, "No title"),
                dest.IsInline,
                GetNotionParent(dest.Parent),
                new Uri(dest.Url),
                dest.Archived,
                GetNotionTextString(dest.Description, "No description"),
                dest.InTrash));

        config.NewConfig<GetNotionPageResponse, NotionPage>()
            .MapWith(src => new NotionPage(
                new NotionPageId(src.Id),
                GetNotionIconString(src.Icon),
                DateTime.Parse(src.CreatedTime),
                new NotionUserId(src.CreatedBy.Id),
                DateTime.Parse(src.LastEditedTime),
                new NotionUserId(src.LastEditedBy.Id),
                ExtractTitleFromPageProperties(src.Properties),
                null,
                GetNotionParent(src.Parent),
                new Uri(src.Url),
                src.Archived,
                src.InTrash));
    }

    private static string ExtractTitleFromPageProperties(Dictionary<string, NotionPropertyContract> properties)
    {
        var title = properties.Where(prop => prop.Value.Type == "title")
            .Select(prop => prop.Value.Title).FirstOrDefault();

        return title is null ? "No title" : GetNotionTextString(title, "No title");
    }

    private static string GetNotionTextString(List<NotionTextContract> text, string defaultValue)
    {
        return text.FirstOrDefault()?.Text.Content ?? defaultValue;
    }

    private static string? GetNotionIconString(NotionIconContract? iconContract)
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

    private static NotionParent GetNotionParent(NotionParentContract parent)
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
