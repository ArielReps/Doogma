using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BasicClient;
using Newtonsoft.Json; // Install via NuGet or use System.Text.Json

class Program
{
    static async Task Main(string[] args)
    {
        var baseUrl = "https://jsonplaceholder.typicode.com/posts";
        // GET example
        var getResponse = await GetExample(baseUrl + "/1");
        Console.WriteLine("GET Response:");
        Console.WriteLine(getResponse);
        Post post = JsonConvert.DeserializeObject<Post>(getResponse);

        //List<Post> posts = new List<Post>();
        //for (int i = 1; i < 100; i++)
        //{
        //    string response = await GetExample(baseUrl + $"/{i}");
        //    posts.Add(JsonConvert.DeserializeObject<Post>(response));
        //}
        Post postData = new Post
        {
            title = "foo",
            body = "bar",
            userId = 1,
        };
        // POST example
        var postResponse = await PostExample(baseUrl, postData);
        Console.WriteLine("\nPOST Response:");
        Console.WriteLine(postResponse);

        // PUT example
        var putResponse = await PutExample(baseUrl + "/1");
        Console.WriteLine("\nPUT Response:");
        Console.WriteLine(putResponse);

        // DELETE example
        var deleteResponse = await DeleteExample(baseUrl + "/1");
        Console.WriteLine("\nDELETE Response:");
        Console.WriteLine(deleteResponse);
    }

    static async Task<string> GetExample(string url)
    {
        using var httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    static async Task<string> PostExample(string url,Post postData)
    {
        using var httpClient = new HttpClient();

        
        var jsonContent = JsonConvert.SerializeObject(postData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    static async Task<string> PutExample(string url)
    {
        using var httpClient = new HttpClient();

        var putData = new
        {
            id = 1,
            title = "updated title",
            body = "updated body",
            userId = 1
        };
        var jsonContent = JsonConvert.SerializeObject(putData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PutAsync(url, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    static async Task<string> DeleteExample(string url)
    {
        using var httpClient = new HttpClient();

        HttpResponseMessage response = await httpClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
        return response.StatusCode.ToString();
    }
}
