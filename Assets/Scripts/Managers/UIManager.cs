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

    [SerializeField]
    private InputField newWordNameInput;
    [SerializeField]
    private InputField newWordDescriptionInput;
    [SerializeField]
    private Text errorMessage;

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

    public void ResetErrorMessage()
    {
        errorMessage.text = "";
    }

}
