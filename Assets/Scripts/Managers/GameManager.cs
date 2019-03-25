using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public UIManager uim;
    [SerializeField]
    private WordAtlas wat;

    private void Awake()
    {
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
        UpdateResults();
    }

    public void AddNewEntry()
    {
        DictionaryEntry newEntry = uim.ValidateNewEntry();

        if(newEntry != null)
        {
            if(wat.ValidateNewEntry(newEntry.Name))
            {
                wat.AddEntry(newEntry);
                uim.ClearNewEntry();
                UpdateResults();
            }
            else
            {
                uim.ShowAleradyExistsError();
            }
        }
    }

    public void DeleteEntry()
    {
        DictionaryEntry elem = uim.selectedElement;
        wat.RemoveEntry(elem.Name);
        uim.CloseOptionsPanel();
        UpdateResults();
    }

    public void UpdateEntry()
    {
        DictionaryEntry newEntry = uim.ValidateDescription();

        if(newEntry!= null)
        {
            wat.UpdateEntry(newEntry);
            uim.CloseEditPanel();
            uim.CloseOptionsPanel();
            UpdateResults();
        }
    }

    private void UpdateResults()
    {
        uim.PopulateResultList(wat.wordsDictionary);
    }
}
