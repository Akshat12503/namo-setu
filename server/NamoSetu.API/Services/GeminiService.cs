using System.Text;
using System.Text.Json;
using NamoSetu.API.DTOs.AI;

namespace NamoSetu.API.Services;

public class GeminiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly string _apiKey;
    private readonly string _model;
    private const string BaseUrl = "https://generativelanguage.googleapis.com/v1beta/models";

    public GeminiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
        _apiKey = _config["Gemini:ApiKey"]!;
        _model = _config["Gemini:Model"] ?? "gemini-2.5-flash";
    }

    // Core method to call Gemini API
    private async Task<string> CallGeminiAsync(string systemPrompt, string userMessage)
    {
        var url = $"{BaseUrl}/{_model}:generateContent?key={_apiKey}";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = $"{systemPrompt}\n\nUser: {userMessage}" }
                    }
                }
            },
            generationConfig = new
            {
                temperature = 0.7,
                maxOutputTokens = 8192,
                topP = 0.9
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Gemini API error: {responseString}");

        using var doc = JsonDocument.Parse(responseString);
        var text = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        return text ?? string.Empty;
    }

    // Chat with conversation history
    private async Task<string> CallGeminiWithHistoryAsync(
        string systemPrompt,
        List<ChatHistoryItem> history,
        string newMessage)
    {
        var url = $"{BaseUrl}/{_model}:generateContent?key={_apiKey}";

        // Build contents array with full history
        var contents = new List<object>();

        // Add history
        foreach (var item in history)
        {
            contents.Add(new
            {
                role = item.Role == "assistant" ? "model" : "user",
                parts = new[] { new { text = item.Message } }
            });
        }

        // Add new message
        contents.Add(new
        {
            role = "user",
            parts = new[] { new { text = newMessage } }
        });

        var requestBody = new
        {
            system_instruction = new
            {
                parts = new[] { new { text = systemPrompt } }
            },
            contents,
            generationConfig = new
            {
                temperature = 0.8,
                maxOutputTokens = 4096,
                topP = 0.9
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Gemini API error: {responseString}");

        using var doc = JsonDocument.Parse(responseString);
        var text = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        return text ?? string.Empty;
    }

    // 1. Generate Pilgrimage Itinerary
    public async Task<string> GenerateItineraryAsync(ItineraryRequestDto request)
    {
        var systemPrompt = @"You are Namo Setu's AI Pilgrimage Planner. Create detailed pilgrimage itineraries for Hindu devotees.
        
Always respond with a valid JSON object with this structure:
{
  ""title"": ""Yatra title"",
  ""destination"": ""destination"",
  ""duration"": ""X days"",
  ""totalBudget"": ""estimated total"",
  ""highlights"": [""key highlights""],
  ""days"": [
    {
      ""day"": 1,
      ""title"": ""Day title"",
      ""temples"": [""temple names""],
      ""activities"": [""activities""],
      ""accommodation"": ""hotel/dharamshala name"",
      ""food"": ""vegetarian food suggestions"",
      ""transport"": ""transport details"",
      ""tips"": ""important tips""
    }
  ],
  ""packingList"": [""items to pack""],
  ""importantContacts"": {""emergency"": ""112"", ""temple"": ""contact""},
  ""bestTimeToVisit"": ""season/months"",
  ""auspiciousDates"": [""dates if any""]
}";

        var userMessage = $@"Plan a pilgrimage to {request.Destination}
Start Date: {request.StartDate:dd MMM yyyy}
End Date: {request.EndDate:dd MMM yyyy}
Number of Travelers: {request.NumTravelers}
Budget: ₹{request.Budget}
Preferred Deity: {request.Deity ?? "Any"}
Special Requirements: {request.SpecialRequirements ?? "None"}
Include Nearby Temples: {request.IncludeNearbyTemples}
Language Preference: {request.Language}

Please create a complete day-by-day pilgrimage itinerary in JSON format.";

        return await CallGeminiAsync(systemPrompt, userMessage);
    }

    // 2. Devotee Chat Assistant
    public async Task<string> ChatWithAssistantAsync(ChatRequestDto request)
    {
        var systemPrompt = @"You are Seva, the AI devotee assistant of Namo Setu pilgrimage app. 
You help Hindu pilgrims with temple information, darshan timings, puja guidance, travel advice, and yatra planning.
Be warm, respectful and use occasional Sanskrit/Hindi words naturally.
For emergencies always say: 'Please call 112 immediately.'
Keep responses concise and helpful. Support both Hindi and English.";

        return await CallGeminiWithHistoryAsync(
            systemPrompt,
            request.History,
            request.Message);
    }

    // 3. Smart Temple Recommender
    public async Task<string> RecommendTemplesAsync(RecommendationRequestDto request)
    {
        var systemPrompt = @"You are a temple recommendation engine for Namo Setu. 
Recommend temples based on user preferences.
Always respond with a valid JSON array of temple recommendations.
Each item must have: name, city, state, deity, reason, bestTimeToVisit, estimatedBudget, specialFeature, distanceFromMajorCity.";

        var userMessage = $@"Recommend {request.NumTemples} temples based on:
Preferred Deity: {request.Deity ?? "Any"}
Preferred State: {request.State ?? "Any"}
Budget: {(request.Budget.HasValue ? $"₹{request.Budget}" : "Any")}
Occasion: {request.Occasion ?? "General pilgrimage"}
Travel Month: {request.TravelMonth ?? "Any"}

Return as JSON array only.";

        return await CallGeminiAsync(systemPrompt, userMessage);
    }

    // 4. Puja Guidance
    public async Task<string> GetPujaGuidanceAsync(string pujaName, string deity, string? language)
    {
        var systemPrompt = @"You are a knowledgeable Hindu priest assistant on Namo Setu app.
Provide detailed puja vidhi (procedure) guidance for devotees.
Include: materials needed, step-by-step procedure, mantras (with meaning), duration, and special tips.
Be respectful and use appropriate Sanskrit terms with translations.";

        var userMessage = $@"Please explain the complete procedure for {pujaName} for {deity}.
Language preference: {language ?? "English"}
Include materials needed, mantras, steps and tips.";

        return await CallGeminiAsync(systemPrompt, userMessage);
    }
}