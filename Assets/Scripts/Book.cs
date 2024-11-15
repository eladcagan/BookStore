using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
     #region Edit Panel
     [SerializeField]
     private GameObject _editPanel;
     [SerializeField]
     private GameObject _editButton;
     [SerializeField]
     private TMP_InputField _inputNameField;
     [SerializeField]
     private TMP_InputField _inputPriceField;
     #endregion

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
     [SerializeField]
     private Renderer _bookRenderer;
     private Tween _movementTween;
     private Tween _rotationTween;

     private string _name;
     private string _description;
     private float _price;
     private float _newPrice;
     private Vector3 _initialPosition;

     public void InitializeBook(BookServerData bookData, Color bookColor)
     {
          _name = bookData.name;
          _description = bookData.description;
          _price = bookData.price;
          _newPrice = _price;
          _initialPosition = transform.localPosition;
          _bookRenderer.material.color = bookColor;
          InitializeBookVisuals();
     }

     //Can be used to return the updated book data to server (out of scope)
     public BookServerData GetBookData()
     {
          BookServerData bookData = new BookServerData
          {
               name = _name,
               description = _description,
               price = _price
          };

          return bookData;
     }

     #region Buttons

     public void OnBookSelected()
     {
          KillTweens();
          _rotationTween = transform.DOLocalRotate(_bookShowRotation, _bookShowDuration);
          _movementTween = transform.DOLocalMove(_bookShowPosition, _bookShowDuration).OnComplete(() =>
          {
               _editButton.SetActive(true);
          });

     }

     public void OnBookUnselected()
     {
          KillTweens();
          _editButton.SetActive(false);
          _rotationTween =transform.DOLocalRotate(Vector3.zero, _bookShowDuration);
          _movementTween = transform.DOLocalMove(_initialPosition, _bookShowDuration);
     }

     public void OnEditButtonClicked()
     {
          _editPanel.SetActive(true);
          _editButton.SetActive(false);
          if (_inputPriceField != null)
          {
               _inputPriceField.onValueChanged.AddListener(ValidateInput);
          }

          _inputNameField.text = string.Empty;
          _inputPriceField.text = string.Empty;
          _newPrice = _price;
     }

     public void OnEditApplied()
     {
          if (_inputPriceField != null)
          {
               _inputPriceField.onValueChanged.RemoveListener(ValidateInput);
          }

          _price = _newPrice;


          //Had to make this check since none of the string.IsEmpty... ect would return false when empty (assuming input field trouble)
          if (_inputNameField.text.ToCharArray().Length > 1)
          {
               _name = _inputNameField.text;
          }

          _editButton.SetActive(true);
          _editPanel.SetActive(false);
          InitializeBookVisuals();
     }

      public void OnEditCanceled()
     {
          if (_inputPriceField != null)
          {
               _inputPriceField.onValueChanged.RemoveListener(ValidateInput);
          }

          _editButton.SetActive(true);
          _editPanel.SetActive(false);
          InitializeBookVisuals();
     }

     #endregion

     private void InitializeBookVisuals()
     {
          _nameText.text = _name;
          _TitleText.text = _name;
          _descriptionText.text = _description;
          _priceText.text = "$" + _price.ToString();

     }

     // Used to ensure valid value for price
     private void ValidateInput(string input)
     {
          // Filter out any non-numeric characters
          string numericInput = "";
          bool hasDecimalPoint = false;

          foreach (char c in input)
          {
               if (char.IsDigit(c))
               {
                    numericInput += c;
               }
               else if (c == '.' && !hasDecimalPoint)
            {
                numericInput += c;
                hasDecimalPoint = true; // Allow only one decimal point
            }
          }

          // Update the input field text to only include valid numeric characters
          _inputPriceField.text = numericInput;

          if (float.TryParse(numericInput, out float result))
          {
               _newPrice = result;
          }
     }

     private void KillTweens()
     {
          _rotationTween.Kill();
          _movementTween.Kill();
     }
}
