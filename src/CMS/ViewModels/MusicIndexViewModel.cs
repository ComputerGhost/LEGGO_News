using Core.Application.Music;

namespace CMS.ViewModels;

public class MusicIndexViewModel
{
    public MusicIndexViewModel(IEnumerable<ListAlbumsQuery.ResponseItemDto> albums)
    {
        Albums = albums;
    }

    public IEnumerable<ListAlbumsQuery.ResponseItemDto> Albums { get; set; } = null!;
}
