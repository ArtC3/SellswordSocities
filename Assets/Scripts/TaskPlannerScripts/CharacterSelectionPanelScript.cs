using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionPanelScript : MonoBehaviour {
    public CharacterTabManager manager;
    public GameObject characterSelection;
    List<CharacterScript> usedCharacters = new List<CharacterScript>();
    // Use this for initialization
    void Start () {
		
	}

    //checks what characters are used
    public void CheckUsed()
    {
        foreach (Button button in manager.ButtonGrouping)
        {
            if(button.GetComponent<TabScript>().character != null)
            {
                usedCharacters.Add(button.GetComponent<TabScript>().character);
            }
        }
    }
	
    //populates the panel with character choices
	public void OnEnable()
    {
        //Debug.Log("Enabled");
        foreach(CharacterScript character in CharacterManagerScript.FullCharacterList)
        {
            if (usedCharacters.Contains(character)) continue;
            GameObject temp = Instantiate(characterSelection);
            temp.GetComponent<TabScript>().character = character;
            temp.GetComponentInChildren<Text>().text = character.name;
            temp.transform.SetParent(this.gameObject.transform);
            temp.GetComponent<Button>().onClick.AddListener(() => OnSelectedCharacter(temp));
        }
    }

    //remove all character options so the list will be refreshed when enabled
    public void OnDisable()
    {
        //Debug.Log("Disabled");
        for (int i = this.transform.childCount; i > 0; i--)
        {
            //Debug.Log(i);
            GameObject temp = this.transform.GetChild(i - 1).gameObject;
            temp.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(temp);
        }
        //refresh the used list
        usedCharacters.Clear();
        CheckUsed();
    }
    public void OnSelectedCharacter(GameObject _btn)
    {
        Debug.Log("In OnSelectedCharacter");
        Debug.Log(_btn.GetComponent<TabScript>().name);
        //character will have to be a nonMono script with a Mono accesser script
        manager.currButton.GetComponent<TabScript>().character = _btn.GetComponent<TabScript>().character;
        manager.currButton.GetComponentInChildren<Text>().text = _btn.GetComponent<TabScript>().character.name;
        this.gameObject.SetActive(false);
    }
}
