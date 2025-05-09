using Easyweb.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Easyweb.Controllers;

public class CustomDataController(IConfiguration config) : Controller
{
    private readonly IConfiguration _config = config;

    [HttpGet("/products")]
    public async Task<IActionResult> Index()
    {
        List<ProductViewModel> products;

        using (var client = new HttpClient())
        {
            var response = await client.GetAsync("https://localhost:7129/api/products");
            var json = await response.Content.ReadAsStringAsync();
            products = JsonSerializer.Deserialize<List<ProductViewModel>>(json);
        }

        return View(products);
    }

    [HttpGet("/qa")]
    public IActionResult AskAI()
    {
        return View();
    }


    [HttpPost("/qa")]
    public async Task<IActionResult> AskAI(string prompt)
    {
        var answer = await GetAIResponse(prompt);
        TempData["AiAnswer"] = answer;

        return Redirect("/qa");
    }

    private async Task<string> GetAIResponse(string prompt)
    {
        var apiKey = _config["OpenAI:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
            return "API key is missing.";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant for a coffee shop website. Respond professionaly and clearly." },
                new { role = "user", content = prompt }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(responseString);
            var root = document.RootElement;

            var messageContent = root
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return messageContent ?? "No response generated.";
        }
        catch (Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }
}
