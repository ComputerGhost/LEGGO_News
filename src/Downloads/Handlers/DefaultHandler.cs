using Microsoft.AspNetCore.Mvc;

namespace Downloads.Handlers;

public class DefaultHandler
{
    public static IResult Handle()
    {
        var html = """
        <!DOCTYPE html>
        <html lang="en">
            <head>
                <meta charset="utf-8">
                <title>Downloads Server</title>
            </head>
            <p>
                This is the downloads server.
                Reference a file using its id as the path.
                For backwards compatibility, old files can be referenced by their filename.
            </p>
        </html>
        """;

        return Results.Content(html, "text/html");
    }
}
