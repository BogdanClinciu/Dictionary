using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class FileManager
{

    private static readonly string filename = "PlayerData.json";

    public static Dictionary<string, string> ImportDictionary()
    {
        Dictionary<string, string> importedDictionary = new Dictionary<string, string>();
        FullDictionary fd = new FullDictionary();

        //read the json file and convert it to FullDictionary
        if(File.Exists(Path.Combine(Application.persistentDataPath,filename)))
        {
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, filename));
            fd = JsonUtility.FromJson<FullDictionary>(json);
        }

        //convert FullDictionary to Dictionary<string,string>
        foreach (var item in fd.fullDict)
        {
            importedDictionary.Add(item.Name, item.Description);
        }

        //return imported dicitionary
        return importedDictionary;
    }

    public static void WriteFile(Dictionary<string,string> dict)
    {
        //Convert dictionary back to FullDictionary
        FullDictionary fd = new FullDictionary();
        foreach (var item in dict)
        {
            fd.fullDict.Add(new DictionaryEntry(item.Key, item.Value));
        }

        //convert FullDictionary to json and write in file
        string json = JsonUtility.ToJson(fd);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, filename), json);
    }
}
