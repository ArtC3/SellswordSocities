using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerScript : MonoBehaviour {
    [SerializeField]
    static public List<CharacterScript> FullCharacterList = new List<CharacterScript>();
    [SerializeField]
    static public List<CharacterScript> PlayersCharacters = new List<CharacterScript>();

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
