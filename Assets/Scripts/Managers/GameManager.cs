using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uim;
    [SerializeField]
    private WordAtlas wat;

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

    private void UpdateResults()
    {
        uim.PopulateResultList(wat.wordsDictionary);
    }
}
