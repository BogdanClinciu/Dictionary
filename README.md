# DictionaryApp
DictionaryApp is a local dictionary manager which allows the user to add, remove, update and search for words. All data is saved locally under User\AppData\LocalLow\

# Quick start
1. Open app
2. Click 'Add new' to add a new word
3. Right click on the new word to open options panel
4. Search through the words by using the input field

# Design
For this project I implemented Model-view-controller (MVC) design pattern. There are 3 major components in the project:
  1. Model (WordAtlas) which is the central component of the pattern, responsible for managing the data, logic and rules of the application;
  2. View (UIManager), the visual representation of the application;
  3. Controller (GameManager) which accepts input from the user and converts it to commands for the model or view.

# Data type
The project imports/exports JSONs for storing the dictionary data.

# Object types
The challange for this project was to implement an english dictionary _using_ a Dictionary. It was a challange because Unity does not serialize Dictionaries so a workaround was needed. To accomplish this, I used 2 structures: DictionaryEntry (which stores the name and the description of a word) and FullDictionary (a list of DictionaryEntry). After the file import, the FullDictionary is converted to a SortedDictionary<string name,string description>. 
