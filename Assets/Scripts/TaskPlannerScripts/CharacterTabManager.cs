using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTabManager : PanelTabsManager {
    public Button characterSelectionButton;
    public GameObject characterSelectionPanel;

    public void Awake()
    {
        characterSelectionPanel.SetActive(false);
    }
    //checks if the character selection button should be displayed
    public void CheckCharacterSelection()
    {
        //if the character name has no reference then display the button
        if (currButton.GetComponent<TabScript>().character == null)
        {
            Debug.Log("Has no character");
            Debug.Log(currPanel.transform.childCount);
            for (int i = currPanel.transform.childCount; i > 0; i--) 
            {
                //Debug.Log(i);
                GameObject temp = currPanel.transform.GetChild(i - 1).gameObject;
                Destroy(temp);
            }
        }
        //if the panel is empty then display button
        if (currPanel.transform.childCount < 1)
        {
            //Debug.Log("Has no children");
            characterSelectionButton.gameObject.SetActive(true);
        }
        else
        {
            characterSelectionButton.gameObject.SetActive(false);
        }
    }

    public void SelectCharacter()
    {
        characterSelectionPanel.SetActive(true);
    }

    /* Causes panels to disappear for some reason
    public void OnSelectedCharacter(GameObject _btn)
    {
        Debug.Log("In OnSelectedCharacter");
        Debug.Log(_btn.GetComponent<TabScript>().character.name);
        currButton.GetComponent<TabScript>().character = _btn.GetComponent<TabScript>().character;
        currButton.GetComponentInChildren<Text>().text = _btn.GetComponent<TabScript>().character.name;
        this.gameObject.SetActive(false);
    }*/
}
