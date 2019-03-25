using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordAtlas : MonoBehaviour
{

    public Dictionary<string, string> wordsDictionary = new Dictionary<string, string>();

    private void Awake()
    {
        wordsDictionary = FileManager.ImportDictionary();
    }

    public bool ValidateNewEntry(string name)
    {
        return !wordsDictionary.ContainsKey(name);
    }

    public void AddEntry(string name, string desc)
    {
        if(!wordsDictionary.ContainsKey(name))
        {
            wordsDictionary.Add(name, desc);
        }
        FileManager.WriteFile(wordsDictionary);
    }

    public void AddEntry(DictionaryEntry newEntry)
    {
        if (!wordsDictionary.ContainsKey(newEntry.Name))
        {
            wordsDictionary.Add(newEntry.Name, newEntry.Description);
        }
        FileManager.WriteFile(wordsDictionary);
    }

    public void RemoveEntry(string name)
    {
        wordsDictionary.Remove(name);
        FileManager.WriteFile(wordsDictionary);
    }

    public void UpdateEntry(DictionaryEntry newEntry)
    {
        Debug.Log(newEntry.Name + "  " + newEntry.Description);
        wordsDictionary[newEntry.Name] = newEntry.Description;
        FileManager.WriteFile(wordsDictionary);
    }
}
