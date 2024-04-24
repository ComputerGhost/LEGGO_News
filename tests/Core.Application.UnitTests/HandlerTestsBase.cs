namespace Core.Application.UnitTests;
public class HandlerTestsBase
{
    protected const int INCONSEQUENTIAL_ID = -1;
    protected DateOnly INCONSEQUENTIAL_DATE = DateOnly.MaxValue;

    protected Stream CreateGoodImageStream()
    {
        const string pngData_base64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVQYV2NgYAAAAAMAAWgmWQ0AAAAASUVORK5CYII=";
        var pngData = Convert.FromBase64String(pngData_base64);
        return new MemoryStream(pngData);
    }
}
