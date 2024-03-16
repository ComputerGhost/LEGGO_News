using FluentValidation;
using System.Text.RegularExpressions;

namespace Core.Music;
internal class CreateAlbumCommandValidator : AbstractValidator<CreateAlbumCommand>
{
    public CreateAlbumCommandValidator()
    {
        RuleFor(command => command.AlbumArtFileName)
            .NotEmpty()
            .MaximumLength(50)
            .Must(IsValidFileName).WithMessage("The filename is not valid.")
            ;
    }

    private static bool IsValidFileName(string fileName)
    {
        var invalidChars = new string(Path.GetInvalidFileNameChars());
        var pattern = "[" + Regex.Escape(invalidChars) + "]";
        return !Regex.IsMatch(fileName, pattern);
    }
}
