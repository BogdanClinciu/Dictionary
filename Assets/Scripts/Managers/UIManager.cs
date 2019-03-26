using System.Collections;
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

        //force update canvas to reposition elements in UI
        Debug.Log("Canvas forced");
        LayoutRebuilder.ForceRebuildLayoutImmediate(resultsContainer.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
    }

    public void RevertOrder()
    {
        InitializeElementsList();
        for (int i = 0; i < entryElementList.Count; i++)
        {
            entryElementList[i].transform.SetSiblingIndex(entryElementList.Count - i - 1);
        }
        //entryElementList.Reverse();
    }

    private void InitializeElementsList()
    {
        entryElementList = new List<DictionaryUIElement>();
        foreach (DictionaryUIElement item in resultsContainer.GetComponentsInChildren<DictionaryUIElement>())
        {
            entryElementList.Add(item);
        }
    }
    #region New words
    public DictionaryEntry ValidateNewEntry()
    {
        ResetErrorMessage();

        if (string.IsNullOrEmpty(newWordNameInput.text) || string.IsNullOrEmpty(newWordDescriptionInput.text))
        {
            errorMessage.text = "Fields should not be empty!";
            return null;
        }

        return new DictionaryEntry(newWordNameInput.text, newWordDescriptionInput.text);
    }

    public void ShowAleradyExistsError()
    {
        errorMessage.text = "Word already exists!";
    }

    public void ClearNewEntry()
    {
        newWordNameInput.text = "";
        newWordDescriptionInput.text = "";
    }
    #endregion

    #region Options panel

    public void OpenOptionsPanel(DictionaryEntry elem)
    {
        selectedElement = elem;
        optionsPanel.SetActive(true);
        SetOptionsAnchor();
        optionsDropdown.transform.position = Input.mousePosition;
    }

    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false);
    }

    public void OpenEditPanel()
    {
        editPanel.SetActive(true);
        wordName.text = selectedElement.Name;
        newDescriptionInput.text = selectedElement.Description;
    }

    public DictionaryEntry ValidateDescription()
    {
        ResetErrorMessage();

        if (string.IsNullOrEmpty(newDescriptionInput.text))
        {
            errorMessage.text = "Description should not be empty!";
            return null;
        }

        return new DictionaryEntry(selectedElement.Name, newDescriptionInput.text);
    }

    public void CloseEditPanel()
    {
        editPanel.SetActive(false);
        ResetErrorMessage();
    }

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


    public void ResetErrorMessage()
    {
        errorMessage.text = "";
    }

}
