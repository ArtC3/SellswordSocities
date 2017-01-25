using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to Button
public class TabScript : MonoBehaviour {
    public GameObject associatedPanel;
    [SerializeField]public CharacterScript character;

    public TabScript(CharacterScript _character)
    {
        character = _character;
    }
}
