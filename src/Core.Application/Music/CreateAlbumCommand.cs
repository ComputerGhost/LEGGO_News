using Core.Application.Common.Models;
using Core.Application.Common.Requirements;
using Core.Domain.Common.Entities;
using Core.Domain.Imaging;
using Core.Domain.Imaging.Ports;
using Core.Domain.Music.Enums;
using Core.Domain.Music.Ports;
using Core.Domain.Users.Enums;
using FluentValidation;
using MediatR;
using MediatR.Behaviors.Authorization;

namespace Core.Application.Music;
public class CreateAlbumCommand : IRequest<int>
{
    public AlbumType AlbumType { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public ImageUpload AlbumArt { get; set; } = null!;

    internal class Authorizer : AbstractRequestAuthorizer<CreateAlbumCommand>
    {
        public override void BuildPolicy(CreateAlbumCommand request)
        {
            UseRequirement(new MustHaveRoleRequirement(UserRole.Editor));
        }
    }

    internal class Handler : IRequestHandler<CreateAlbumCommand, int>
    {
        private readonly IMusicDatabasePort _databaseAdapter;
        private readonly IFileSystemPort _fileSystemAdapter;

        public Handler(IMusicDatabasePort databaseAdapter, IFileSystemPort fileSystemAdapter)
        {
            _databaseAdapter = databaseAdapter;
            _fileSystemAdapter = fileSystemAdapter;
        }

        public async Task<int> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var imageSaver = new ImageSaver(_fileSystemAdapter);

            return await _databaseAdapter.Create(new AlbumEntity
            {
                AlbumType = await _databaseAdapter.FetchAlbumType(request.AlbumType.ToString()),
                Title = request.Title,
                Artist = request.Artist,
                ReleaseDate = request.ReleaseDate,
                Image = await imageSaver.SaveToFileSystem(request.AlbumArt.FileName, request.AlbumArt.Stream),
            });
        }
    }

    internal class Validator : AbstractValidator<CreateAlbumCommand>
    {
        public Validator(IValidator<ImageUpload> imageUploadValidator)
        {
            RuleFor(command => command.Title)
                .Length(1, 50);

            RuleFor(command => command.Artist)
                .Length(1, 50);

            RuleFor(command => command.AlbumArt)
                .NotNull()
                .SetValidator(imageUploadValidator);
        }
    }
}
