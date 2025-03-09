using OperationOOP.Core.Services;

namespace OperationOOP.Api.Endpoints;

// SortByAge-klassen implementerar ett API-endpoint för att sortera bonsaiträd efter ålder
public class SortByAge : IEndpoint
{
    // MapEndpoint-metoden konfigurerar endpointen och länkar den till en specifik URL
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/bonsais/sorted-by-age", Handle) // GET-endpoint för att sortera bonsai-träd efter ålder
        .WithSummary("Sort bonsais by age"); // Sammanfattning av endpointen för API-dokumentation

    // Response-record som representerar utdata från endpointen
    public record Response(
        int Id,
        string Name,
        int AgeYears
    );

    // Handle-metoden hanterar inkommande förfrågningar och returnerar bonsaiträd sorterade efter ålder
    private static IEnumerable<Response> Handle(BonsaiService service)
    {
        // Använder BonsaiService för att sortera bonsaiträd efter ålder i stigande ordning
        return service.SortBonsaisByAge()
            .Select(b => new Response(
                Id: b.Id,
                Name: b.Name,
                AgeYears: b.AgeYears
            ));
    }
}