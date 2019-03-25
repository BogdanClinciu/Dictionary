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

    public void PopulateResultList(Dictionary<string, string> words)
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
        }

        //force update canvas to reposition elements in UI
        Canvas.ForceUpdateCanvases();
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
        Debug.Log(newDescriptionInput.text);
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


    #endregion
    public void ResetErrorMessage()
    {
        errorMessage.text = "";
    }

}
