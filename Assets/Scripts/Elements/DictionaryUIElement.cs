using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DictionaryUIElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text descriptionText;

    public void SetValues(string name, string desc)
    {
        nameText.text = name;
        descriptionText.text = desc;
    }

    public void UpdateDescription(string newDescription)
    {
        descriptionText.text = newDescription;
    }
    
    /// <summary>
    /// Track right mouse click to open options panel
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameManager.instance.uiManagerComponent.OpenOptionsPanel(new DictionaryEntry(nameText.text, descriptionText.text));
        }
    }
}
