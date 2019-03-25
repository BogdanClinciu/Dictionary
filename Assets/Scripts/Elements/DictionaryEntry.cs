using System;
using System.Collections.Generic;

[Serializable]
public class DictionaryEntry
{
    public string Name;
    public string Description;

    public DictionaryEntry(string name, string desc)
    {
        Name = name;
        Description = desc;
    }
}

[Serializable]
public class FullDictionary
{
    public List<DictionaryEntry> fullDict = new List<DictionaryEntry>();
}