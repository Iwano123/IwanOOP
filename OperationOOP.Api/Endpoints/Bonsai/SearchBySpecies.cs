using OperationOOP.Core.Services;

namespace OperationOOP.Api.Endpoints;

// SearchBySpecies-klassen implementerar ett API-endpoint för att söka efter bonsaiträd baserat på deras art
public class SearchBySpecies : IEndpoint
{
    // MapEndpoint-metoden konfigurerar endpointen och länkar den till en specifik URL
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/bonsais/search-by-species", Handle) // GET-endpoint för att söka efter bonsaiträd baserat på art
        .WithSummary("Search bonsais by species"); // Sammanfattning av endpointen för API-dokumentation

    // Request-record som representerar inkommande data till endpointen
    public record Request(string Species); // Tar emot en sträng som representerar arten att söka efter

    // Response-record som representerar utdata från endpointen
    public record Response(
        int Id,
        string Name,
        string Species
    );

    // Handle-metoden hanterar inkommande förfrågningar och returnerar bonsaiträd som matchar sökningen
    private static IEnumerable<Response> Handle([AsParameters] Request request, BonsaiService service)
    {
        // Använder BonsaiService för att söka efter bonsaiträd baserat på arten
        return service.SearchBonsaisBySpecies(request.Species)
            .Select(b => new Response(
                Id: b.Id,
                Name: b.Name,
                Species: b.Species
            ));
    }
}