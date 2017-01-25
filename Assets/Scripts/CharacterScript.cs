using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript {
    public int ID;
    public string name;
	
    public CharacterScript(int _id, string _name)
    {
        ID = _id;
        name = _name;
    }
    public CharacterScript(CharacterScript _character)
    {
        ID = _character.ID;
        name = _character.name;
    }
    public void CharacterScriptFiller(CharacterScript _CS)
    {
        ID = _CS.ID;
        name = _CS.name;
    }
}
