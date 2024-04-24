using Core.Application.Common.Exceptions;
using Core.Application.Common.Models;
using Core.Application.Common.Requirements;
using Core.Domain.Imaging;
using Core.Domain.Music.Enums;
using Core.Domain.Music.Ports;
using Core.Domain.Users.Enums;
using FluentValidation;
using MediatR;
using MediatR.Behaviors.Authorization;

namespace Core.Application.Music;
public class UpdateAlbumCommand : IRequest
{
    public int Id { get; set; }

    public AlbumType AlbumType { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public ImageUpload? AlbumArt { get; set; }

    internal class Authorizer : AbstractRequestAuthorizer<UpdateAlbumCommand>
    {
        public override void BuildPolicy(UpdateAlbumCommand request)
        {
            UseRequirement(new MustHaveRoleRequirement(UserRole.Editor));
        }
    }

    internal class Handler : IRequestHandler<UpdateAlbumCommand>
    {
        private readonly IImagingFacade _imagingFacade;
        private readonly IMusicDatabasePort _databaseAdapter;

        public Handler(IImagingFacade imagingFacade, IMusicDatabasePort databaseAdapter)
        {
            _imagingFacade = imagingFacade;
            _databaseAdapter = databaseAdapter;
        }

        public async Task Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var existingAlbum = await _databaseAdapter.FetchAlbum(request.Id);
            if (existingAlbum == null)
            {
                throw new NotFoundException();
            }

            existingAlbum.AlbumType = await _databaseAdapter.FetchAlbumType(request.AlbumType.ToString());
            existingAlbum.Title = request.Title;
            existingAlbum.Artist = request.Artist;
            existingAlbum.ReleaseDate = request.ReleaseDate;

            if (request.AlbumArt != null)
            {
                existingAlbum.Image = await _imagingFacade.SaveToFileSystem(request.AlbumArt.FileName, request.AlbumArt.Stream);
            }

            await _databaseAdapter.Update(existingAlbum);
        }
    }

    internal class Validator : AbstractValidator<UpdateAlbumCommand>
    {
        public Validator(IValidator<ImageUpload> imageUploadValidator)
        {
            RuleFor(command => command.Title)
                .Length(1, 50);

            RuleFor(command => command.Artist)
                .Length(1, 50);

            RuleFor(command => command.AlbumArt)
                .SetValidator(imageUploadValidator!)
                .When(command => command.AlbumArt != null);
        }
    }
}
