using UnityEngine;
using TMPro;

public class Book : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
     [SerializeField]
    private TextMeshProUGUI _TitleText;
    [SerializeField]
    private TextMeshProUGUI _descriptionText;
    [SerializeField]
    private TextMeshProUGUI _priceText;

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
            Name = _name,
            Description = _description,
            Price = _price
        };

        return bookData;
   }

   private void InitializeBookVisuals()
   {
        _nameText.text = _name;
        _TitleText.text = _name;
        _descriptionText.text = _description;
        _priceText.text = _price.ToString() + "$"; 
   }
}
