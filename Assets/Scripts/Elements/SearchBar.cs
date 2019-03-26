using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchBar : MonoBehaviour
{
    SortedDictionary<string, string> resultDictionary = new SortedDictionary<string, string>();

    private void Awake()
    {
        //add listener for input field value change
        GetComponent<InputField>().onValueChanged.AddListener(OnInputValueChanged);
    }

    /// <summary>
    /// Get the value from input field and pass it for processing
    /// </summary>
    /// <param name="newText"></param>
    private void OnInputValueChanged(string newText)
    {
        GetResults(newText);
        GameManager.instance.uiManagerComponent.PopulateResultList(resultDictionary);
    }

    /// <summary>
    /// Get dictionary of all elements that contain the inputed string
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Returns SortedDictionary with valid elements</returns>
    private SortedDictionary<string, string> GetResults(string input)
    {
        resultDictionary = new SortedDictionary<string, string>();

        foreach (var item in GameManager.instance.wordAtlasComponent.wordsDictionary)
        {
            if (item.Key.ToLower().IndexOf(input.ToLower()) >= 0)
            {
                resultDictionary.Add(item.Key, item.Value);
            }
        }

        return resultDictionary;
    }
}
