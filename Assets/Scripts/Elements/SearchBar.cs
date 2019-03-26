using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
public class SearchBar : MonoBehaviour
{
    SortedDictionary<string, string> resultDictionary = new SortedDictionary<string, string>();

    private void Awake()
    {
        GetComponent<InputField>().onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string newText)
    {
        GetResults(newText);
        GameManager.instance.uim.PopulateResultList(resultDictionary);
    }



    private SortedDictionary<string, string> GetResults(string input)
    {
        resultDictionary = new SortedDictionary<string, string>();
        foreach (var item in GameManager.instance.wat.wordsDictionary)
        {
            if (item.Key.ToLower().IndexOf(input.ToLower()) >= 0)
                resultDictionary.Add(item.Key,item.Value);
        }

        return resultDictionary;
    }
}
