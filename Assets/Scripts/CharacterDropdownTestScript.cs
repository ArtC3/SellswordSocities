using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDropdownTestScript : MonoBehaviour {
    [SerializeField] public static List<CharacterScript> FullCharacterList = new List<CharacterScript>();
    //For testing saving characters
    public Dropdown dropDown;
    public InputField inputField;
	// Use this for initialization
	void Start () {
        PopulateDropDown();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PopulateDropDown()
    {
        dropDown.options.Clear();
        foreach (var character in FullCharacterList)
        {
            dropDown.options.Add(new Dropdown.OptionData(character.name));
        }
        dropDown.RefreshShownValue();
    }

    public void RenameCharacter()
    {
        foreach (var character in FullCharacterList)
        {
            if (character.ID == (dropDown.value + 1))
            {
                character.name = inputField.text;
            }
        }
        PopulateDropDown();
    }
}
