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
        for (int index = 0; index < shelfData.Count; index++)
        {
            Vector3 bookPosition = GetBookPosition(index);
            var book = Instantiate(_bookPrefab, _bookParent) as Book;
            book.transform. position = bookPosition;
            book.InitializeBook(shelfData[index]);
        }
    }

    // Position the books from the center outwards
    private Vector3 GetBookPosition(int bookIndex)
    {
        int direction = bookIndex % 2 == 0 ? 1 : -1;
        float offset = (bookIndex +1 )/2 * direction * _bookOffset;
        Vector3 bookPosition = new Vector3 (_bookParent.position.x + offset, _bookParent.position.y, _bookParent.position.z);
        return bookPosition;
    }
}
