using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class StoreManager : MonoBehaviour
{
     [SerializeField]
    private bool _useMockData;


    [SerializeField]
    private ShelfManager _shelf;

    [SerializeField]
    private List<BookServerData> _mockData;

    //private List<BookData> _shelfData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        TryGetShelfData();
    }

    // Update is called once per frame
    private async void TryGetShelfData()
    {
        List<BookServerData> booksData = await ServerManager.Instance.FetchDataAsync("products");

        if(booksData.Count == 0 || _useMockData)
        {
            InitializeShelf(_mockData);
            Debug.Log("Server data could not be fetched using Mock Data");
        } 
        else
        {
            InitializeShelf(booksData);
            Debug.Log("Server data was fetched successfuly");

        }
    }

    private void InitializeShelf(List<BookServerData> booksData)
    {
        List<BookServerData> randomBooksData = GetRandomData(booksData);
        _shelf.InitializeShelf(randomBooksData);
    }

    private List<BookServerData> GetRandomData(List<BookServerData> booksData)
    {
        List<BookServerData> randomBooksData = new List<BookServerData>();
        int randomBooksAmount = Random.Range(1,4);

        List<int> availableIndices = Enumerable.Range(0, booksData.Count).ToList();
        availableIndices = availableIndices.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < randomBooksAmount && i < availableIndices.Count; i++)
        {
            int randomIndex = availableIndices[i];
            randomBooksData.Add(booksData[randomIndex]);
        }   
       
        return randomBooksData;
    }
}


