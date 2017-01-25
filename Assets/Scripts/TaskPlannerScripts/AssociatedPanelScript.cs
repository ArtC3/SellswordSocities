using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssociatedPanelScript : MonoBehaviour {
    public CharacterTabManager manager;

    void Awake()
    {
        manager = this.gameObject.transform.parent.GetComponent<CharacterTabManager>();
    }

	public void OnTransformChildrenChanged()
    {
        manager.CheckCharacterSelection();
    }
}
