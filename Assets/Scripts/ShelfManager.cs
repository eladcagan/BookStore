using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    [SerializeField]
    private Book _bookPrefab;

    [SerializeField]
    private Transform _bookParent;
    [SerializeField]
    private float _bookOffset = 0.5f;


    public void InitializeShelf(List<BookServerData> shelfData)
    {
        Debug.Log("shelf initialized");
        for (int index = 0; index < shelfData.Count; index++)
        {
            Vector3 bookPosition = GetBookPosition(index);
            var book = Instantiate(_bookPrefab) as Book;
            book.transform. position = bookPosition;
            book.InitializeBook(shelfData[index]);
        }
    }

    private Vector3 GetBookPosition(int bookIndex)
    {
        float offset = _bookOffset * Mathf.Pow(-1, bookIndex);
        Vector3 bookPosition = new Vector3 (_bookParent.position.x + offset, _bookParent.position.y, _bookParent.position.z);
        return bookPosition;
    }
}
