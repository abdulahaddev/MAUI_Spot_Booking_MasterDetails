using MAUI_MasterDetails.Model;
using System.Net.Http.Json;

namespace MAUI_MasterDetails.Service;

public class BookingService
{
    private const string BASE_URL = "http://localhost:5047/api/BookingEntries/";

    private HttpClient _httpClient;
    protected HttpClient HttpClientInstance => _httpClient ??= new HttpClient();

    public async Task<List<Spot>> GetSpots()
    {
        var url = BASE_URL + "GetSpots";
        return await HttpClientInstance.GetFromJsonAsync<List<Spot>>(url);
    }

    public async Task<List<Client>> GetClients()
    {
        var url = BASE_URL + "GetClients";
        var data = await HttpClientInstance.GetFromJsonAsync<List<Client>>(url);

        return data;
    }

    public async Task<Client> GetClientById(int clientId)
    {
        var url = BASE_URL + "GetClientById/" + clientId;
        var data = await HttpClientInstance.GetFromJsonAsync<Client>(url);

        return data;
    }

    public async Task<bool> DeleteClient(int clientId)
    {
        var url = BASE_URL + "Delete/" + clientId;
        var response = await HttpClientInstance.DeleteAsync(url);

        return response.IsSuccessStatusCode;
    }

    public async Task<HttpResponseMessage> AddBooking(MultipartFormDataContent data)
    {
        try
        {
            return await HttpClientInstance.PostAsync(BASE_URL, data);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<HttpResponseMessage> UpdateBooking(MultipartFormDataContent data)
    {
        try
        {
            return await HttpClientInstance.PutAsync(BASE_URL, data);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
