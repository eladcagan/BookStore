using UnityEngine;
using TMPro;
using DG.Tweening;

public class Book : MonoBehaviour
{
     [SerializeField]
    private GameObject _editPanel;
     [SerializeField]
    private GameObject _editButton;
     [SerializeField]
    private TextMeshProUGUI _inputField;


    [SerializeField]
    private Vector3 _bookShowPosition;
    [SerializeField]
    private float _bookShowDuration;
    [SerializeField]
    private Vector3 _bookShowRotation;
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
    private Vector3 _initialPosition;
    private Vector3 _initialRotation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void InitializeBook(BookServerData bookData)
   {
        _name = bookData.Name;
        _description = bookData.Name;
        _price = bookData.Price;
        _initialPosition = transform.localPosition;
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

   public void OnBookSelected()
   {
        transform.DOLocalRotate(_bookShowRotation, _bookShowDuration);
        transform.DOLocalMove(_bookShowPosition, _bookShowDuration).OnComplete(()=> 
        {
            _editButton.SetActive(true);
        });
        
   }

   public void OnBookUnselected()
   {
        _editButton.SetActive(false);
        transform.DOLocalRotate(Vector3.zero, _bookShowDuration);
        transform.DOLocalMove(_initialPosition, _bookShowDuration);
   }

    public void OnEditButtonClicked()
   {
        _editPanel.SetActive(true);
        _editButton.SetActive(false);
   }

   public void OnEditApplied()
   {
       _name = _inputField.text;
       _editButton.SetActive(true);
       _editPanel.SetActive(false);
       InitializeBookVisuals();

   }
}
