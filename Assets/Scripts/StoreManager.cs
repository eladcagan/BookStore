using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;


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
        if(booksData == null)
        {
            InitializeShelf(_mockData);
            Debug.Log("Server data could not be fetched using Mock Data");
        } else
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
        for (int i = 0; i < randomBooksAmount; i++)
        {
            int randomBook = Random.Range(0,booksData.Count);
            randomBooksData.Add(booksData[randomBook]);
        }

        return randomBooksData;
    }
}


[System.Serializable]
public class BookServerData
{
    public string Name;
    public string Description;
    public float Price;
}

[System.Serializable]
public class ShelfServerData
{
    public List<BookServerData> BooksData;
}