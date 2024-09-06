using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class TimeFetcher : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();

    private static async Task<DateTime?> FetchTimeFromUrl(string url)
    {
        try
        {
            var response = await client.GetAsync(url);
            if (response.Headers.Date.HasValue)
            {
                return response.Headers.Date.Value.DateTime.ToLocalTime();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error fetching time from {url}: {ex.Message}");
        }
        return null;
    }

    public static async Task<DateTime> GetExactTime()
    {
        var urls = new[]
        {
            "https://time100.ru/",
            "https://time.is/",
            "https://www.microsoft.com/",
            "https://www.google.com/"
        };

        DateTime? bestTime = null;

        foreach (var url in urls)
        {
            var time = await FetchTimeFromUrl(url);
            if (time.HasValue)
            {
                if (bestTime == null || (time.Value - DateTime.Now).Duration() < (bestTime.Value - DateTime.Now).Duration())
                {
                    bestTime = time;
                }
            }
        }

        if (bestTime.HasValue)
        {
            return bestTime.Value;
        }
        else
        {
            Debug.LogWarning("All time sources failed. Returning local time.");
            return DateTime.Now;
        }
    }
}