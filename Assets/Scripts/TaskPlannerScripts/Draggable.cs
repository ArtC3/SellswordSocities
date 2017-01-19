using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform parentToReturn = null;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnBeginDrag(PointerEventData eventData)
    {
        //makes sure the Draggable item gets a copy of its last parent
        parentToReturn = this.transform.parent;
        //necessary for forcing the item to "snap" back to their last parent and refresh the list
        //without, the item can be placed anywhere in the panel since they never leave their parent and won't force a snap
        this.transform.SetParent(this.transform.parent.parent);

        //Sets Blocks Raycast to off to allow pointer to raycast to any canvas underneith it
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    //move object with pointer
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }
    //When the dragging has ended we set the new parent and reset the raycast block
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentToReturn);
        //Resets Blocks Raycast to on to allow pointer to raycast the object
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
    }
}
