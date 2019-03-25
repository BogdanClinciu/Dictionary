using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryUIElement : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text descriptionText;

    public void SetValues(string name, string desc)
    {
        nameText.text = name;
        descriptionText.text = desc;
    }

    public void UpdateDescription(string newDescription)
    {
        descriptionText.text = newDescription;
    }
}
