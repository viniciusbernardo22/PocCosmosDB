using Microsoft.Azure.Cosmos;
using PocCosmosDb.BlazorServer.DTOs;
using PocCosmosDb.BlazorServer.Models;

namespace PocCosmosDb.BlazorServer.Services;

public class EnginnerService : IEnginnerService {
    
    // LOCAL COSMOSDB INSTANCE RUNNING IN MY MACHINE
    private string _PCS = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    private string _CosmoDbName = "Contractors";
    private string _CosmosDbContainer = "Engineers";
    
    private Container GetContainerClient()
    {
        var client = new CosmosClient(_PCS);

        Container container = client.GetContainer(_CosmoDbName, _CosmosDbContainer);

        return container;
    }
    
    public async Task<List<Engineer>> GetEnginnersAsync()
    {
        var enginners = new List<Engineer>();
        try
        {
            var client = GetContainerClient();
            
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");

            FeedIterator<Engineer> queryResultSetIterator = client.GetItemQueryIterator<Engineer>(queryDefinition);

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Engineer> currentResultSet = await queryResultSetIterator.ReadNextAsync();

                foreach (Engineer engineer in currentResultSet)
                {
                    enginners.Add(engineer);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return enginners;
    }

    public async Task AddEnginnerAsync(Engineer engineer)
    {
        try
        {
            var client = GetContainerClient();
            engineer.id = Guid.NewGuid();
            var response = await client.CreateItemAsync(engineer, new PartitionKey(engineer.id.ToString()));
        }
        catch (Exception e)
        {
           Console.WriteLine(e.Message);
        }
    }
}

