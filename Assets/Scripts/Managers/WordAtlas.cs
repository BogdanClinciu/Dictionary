using System.Collections.Generic;
using UnityEngine;

public class WordAtlas : MonoBehaviour
{

    public SortedDictionary<string, string> wordsDictionary = new SortedDictionary<string, string>();

    private void Awake()
    {
        //import dictionary from json
        wordsDictionary = FileManager.ImportDictionary();
    }

    /// <summary>
    /// Check if input name is already in atlas
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool ValidateNewEntry(string name)
    {
        return !wordsDictionary.ContainsKey(name);
    }

    /// <summary>
    /// Add word to atlas; save atlas in file
    /// </summary>
    /// <param name="name"></param>
    /// <param name="desc"></param>
    public void AddEntry(string name, string desc)
    {
        wordsDictionary.Add(name, desc);

        FileManager.WriteFile(wordsDictionary);
    }

    /// <summary>
    /// Add word to atlas; save atlas in file
    /// </summary>
    /// <param name="newEntry"></param>
    public void AddEntry(DictionaryEntry newEntry)
    {
        wordsDictionary.Add(Utils.FirstCharToUpper(newEntry.Name), newEntry.Description);

        FileManager.WriteFile(wordsDictionary);
    }

    /// <summary>
    /// Remove word from atlas; save atlas in file
    /// </summary>
    /// <param name="name"></param>
    public void RemoveEntry(string name)
    {
        wordsDictionary.Remove(name);
        FileManager.WriteFile(wordsDictionary);
    }

    /// <summary>
    /// Update description of word; save atlas in file
    /// </summary>
    /// <param name="newEntry"></param>
    public void UpdateEntry(DictionaryEntry newEntry)
    {
        wordsDictionary[newEntry.Name] = newEntry.Description;
        FileManager.WriteFile(wordsDictionary);
    }
}
