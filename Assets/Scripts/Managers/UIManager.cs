using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private DictionaryUIElement entryPrefab;
    [SerializeField]
    private GameObject resultsContainer;
    private List<DictionaryUIElement> entryElementList = new List<DictionaryUIElement>();

    [Space]
    [Header("Add new word")]

    [SerializeField]
    private InputField newWordNameInput;
    [SerializeField]
    private InputField newWordDescriptionInput;
    [SerializeField]
    private Text errorMessage;

    [Space]
    [Header("Options panel")]

    [HideInInspector]
    public DictionaryEntry selectedElement;
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private GameObject optionsDropdown;
    [SerializeField]
    private GameObject editPanel;
    [SerializeField]
    private Text wordName;
    [SerializeField]
    private InputField newDescriptionInput;

    /// <summary>
    /// Show the current element selection on screen
    /// </summary>
    /// <param name="words"></param>
    public void PopulateResultList(SortedDictionary<string, string> words)
    {
        //clean old entries
        foreach (Transform item in resultsContainer.transform)
        {
            Destroy(item.gameObject);
        }

        //add new entries
        foreach (var item in words)
        {
            DictionaryUIElement elem = Instantiate(entryPrefab);
            elem.SetValues(item.Key, item.Value);
            elem.transform.parent = resultsContainer.transform;
            entryElementList.Add(elem);
        }

        //force update canvas to reposition elements in UI and rebuild layouts
        LayoutRebuilder.ForceRebuildLayoutImmediate(resultsContainer.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
    }

    /// <summary>
    /// Toggle between element sorting A-Z/Z-A
    /// </summary>
    public void RevertOrder()
    {
        //get refference to the elements on screen
        InitializeElementsList();

        //revert the order in hierarchy to avoid instantiating the same elements in different order
        for (int i = 0; i < entryElementList.Count; i++)
        {
            entryElementList[i].transform.SetSiblingIndex(entryElementList.Count - i - 1);
        }
    }

    /// <summary>
    /// Set refference to the current on-screen words
    /// </summary>
    private void InitializeElementsList()
    {
        entryElementList = new List<DictionaryUIElement>();
        foreach (DictionaryUIElement item in resultsContainer.GetComponentsInChildren<DictionaryUIElement>())
        {
            entryElementList.Add(item);
        }
    }

    #region New words

    /// <summary>
    /// Input validation for new word name and description
    /// </summary>
    /// <returns>Returns false is the input is not valid. Returns new DictionaryEntry otherwise</returns>
    public DictionaryEntry ValidateNewEntry()
    {
        ShowErrorMessage(Constants.ERROR_CLEAR);

        if (string.IsNullOrEmpty(newWordNameInput.text) || string.IsNullOrEmpty(newWordDescriptionInput.text))
        {
            ShowErrorMessage(Constants.ERROR_INVALID_INPUT);
            return null;
        }

        return new DictionaryEntry(newWordNameInput.text, newWordDescriptionInput.text);
    }

    /// <summary>
    /// Show error message 
    /// </summary>
    public void ShowErrorMessage(string error)
    {
        errorMessage.text = error;
    }

    /// <summary>
    /// Reset new word input fields
    /// </summary>
    public void ClearNewEntry()
    {
        newWordNameInput.text = "";
        newWordDescriptionInput.text = "";
    }
    #endregion

    #region Options panel

    /// <summary>
    /// Open options panel; set position to mouse coordinates
    /// </summary>
    /// <param name="elem"></param>
    public void OpenOptionsPanel(DictionaryEntry elem)
    {
        selectedElement = elem;
        optionsPanel.SetActive(true);
        SetOptionsAnchor();
        optionsDropdown.transform.position = Input.mousePosition;
    }

    /// <summary>
    /// Close options panel
    /// </summary>
    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false);
    }

    /// <summary>
    /// Open edit panel; initialize with selected word's name and description
    /// </summary>
    public void OpenEditPanel()
    {
        editPanel.SetActive(true);
        wordName.text = selectedElement.Name;
        newDescriptionInput.text = selectedElement.Description;
    }

    /// <summary>
    /// Validate input description
    /// </summary>
    /// <returns>Return null if the input is not valid. Return new DictionaryEntry otherwise</returns>
    public DictionaryEntry ValidateDescription()
    {
        ShowErrorMessage(Constants.ERROR_CLEAR);

        if (string.IsNullOrEmpty(newDescriptionInput.text))
        {
            ShowErrorMessage(Constants.ERROR_INVALID_INPUT);
            return null;
        }

        return new DictionaryEntry(selectedElement.Name, newDescriptionInput.text);
    }

    /// <summary>
    /// Close edit panel; reset error message
    /// </summary>
    public void CloseEditPanel()
    {
        editPanel.SetActive(false);
        ShowErrorMessage(Constants.ERROR_CLEAR);
    }

    /// <summary>
    /// Set dropdown's pivot based on mouse coordinates to avoid showing the panel outside the screen
    /// </summary>
    private void SetOptionsAnchor()
    {
        float xValue = 0;
        float yValue = 0;
        if (Input.mousePosition.x + optionsDropdown.GetComponent<RectTransform>().sizeDelta.x > Screen.width)
        {
            xValue = 1;
        }
        else
        {
            xValue = 0;
        }

        if (Input.mousePosition.y - optionsDropdown.GetComponent<RectTransform>().sizeDelta.y > 0)
        {
            yValue = 1;
        }
        else
        {
            yValue = 0;
        }

        optionsDropdown.GetComponent<RectTransform>().pivot = new Vector2(xValue, yValue);
    }

    #endregion
}
