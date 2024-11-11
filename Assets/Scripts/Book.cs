using UnityEngine;


public class Book : MonoBehaviour
{
    private string _name;
    private string _description;
    private float _price;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void InitializeBook(BookServerData bookData)
   {
        _name = bookData.Name;
        _description = bookData.Name;
        _price = bookData.Price;

        InitializeBookVisuals();
   }

   public BookServerData GetBookData()
   {
        BookServerData bookData = new BookServerData
        {

        };
        return bookData;
   }

   private void InitializeBookVisuals()
   {

   }
}
