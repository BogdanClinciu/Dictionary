using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public UIManager uiManagerComponent;
    public WordAtlas wordAtlasComponent;

    private void Awake()
    {
        //create singleton instance
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //Display atlas
        UpdateResults();
    }

    /// <summary>
    /// Add new word to dictionary
    /// </summary>
    public void AddNewEntry()
    {
        //validate input field
        DictionaryEntry newEntry = uiManagerComponent.ValidateNewEntry();

        if(newEntry != null)
        {
            //if input is valid, check if the word is already in the dictionary
            if(wordAtlasComponent.ValidateNewEntry(newEntry.Name))
            {
                //add word to atlas, reset input fields, display the new element
                wordAtlasComponent.AddEntry(newEntry);
                uiManagerComponent.ClearNewEntry();
                UpdateResults();
            }
            else
            {
                uiManagerComponent.ShowErrorMessage(Constants.ERROR_ALREADY_EXISTS);
            }
        }
    }

    /// <summary>
    /// Remove selected word from atlas
    /// </summary>
    public void DeleteEntry()
    {
        DictionaryEntry elem = uiManagerComponent.selectedElement;
        
        //remove the word from atlas, close the options panel, update displayed list
        wordAtlasComponent.RemoveEntry(elem.Name);
        uiManagerComponent.CloseOptionsPanel();
        UpdateResults();
    }

    /// <summary>
    /// Update description of selected word
    /// </summary>
    public void UpdateEntry()
    {
        DictionaryEntry newEntry = uiManagerComponent.ValidateDescription();

        if(newEntry!= null)
        {
            //if the description is valid, update it in atlas, close panels, update display
            wordAtlasComponent.UpdateEntry(newEntry);
            uiManagerComponent.CloseEditPanel();
            uiManagerComponent.CloseOptionsPanel();
            UpdateResults();
        }
    }

    /// <summary>
    /// Display atlas
    /// </summary>
    private void UpdateResults()
    {
        uiManagerComponent.PopulateResultList(wordAtlasComponent.wordsDictionary);
    }
}
