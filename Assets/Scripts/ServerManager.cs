using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class ServerManager : MonoBehaviour
{
    private readonly string baseUrl = "https://homework.mocart.io/api";

    public static ServerManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fetch a list of PlayerData from the server (GET)
    public async Task<List<BookServerData>> FetchDataAsync(string endpoint)
    {
        string url = $"{baseUrl}/{endpoint}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                ShelfServerData dataList = JsonUtility.FromJson<ShelfServerData>(jsonResponse);
                return dataList.BooksData;
            }
            else
            {
                Debug.LogError($"Error fetching data: {request.error}");
                return null;
            }
        }
    }
    
    // Update data on the server (POST with JSON)
    public async Task<string> UpdateDataAsync<T>(string endpoint, T data)
    {
        string url = $"{baseUrl}/{endpoint}";
        string jsonData = JsonUtility.ToJson(data);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                return request.downloadHandler.text;
            }
            else
            {
                Debug.LogError($"Error updating data: {request.error}");
                return null;
            }
        }
    }
}