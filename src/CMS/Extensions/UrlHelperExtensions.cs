using CMS.Controllers;
using Core.Common.Imaging;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Extensions;

public static class UrlHelperExtensions
{
    public static string GetThumbnailUrl(this IUrlHelper urlHelper, int id)
    {
        return GetImageUrl(urlHelper, id, ImageWidth.Thumbnail);
    }

    public static string GetImageUrl(this IUrlHelper urlHelper, int id, ImageWidth size)
    {
        var controller = nameof(DownloadsController);
        var action = nameof(DownloadsController.Images);
        var parameters = new
        {
            id,
            size = size.ToString(),
        };

        // We have to remove the suffix until dotnet issue #27204 is closed.
        // See: https://github.com/dotnet/aspnetcore/issues/27204
        controller = controller.Replace("Controller", "");

        return urlHelper.Action(action, controller, parameters)!;
    }
}
