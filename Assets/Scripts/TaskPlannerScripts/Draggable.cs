using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform parentToReturn = null;
    public GameObject placeholder;
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
        //placeholder holds a space in the listing 
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
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
        //if the item is from the TaskChoices then don't bother changing it's placeholder
        if (this.parentToReturn.gameObject.GetComponent<DropZone>().dZType == DropZone.dropZoneTypes.TASKCHOICES) return;
        //cycle through to see if this y is greater and if so move the placeholder above
        for (int i = 0; i < parentToReturn.childCount; i++)
        {
            if(this.transform.position.y > parentToReturn.GetChild(i).position.y)
            {
                placeholder.transform.SetSiblingIndex(i);
                break;
            }
        }
    }
    //When the dragging has ended we set the new parent and reset the raycast block
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentToReturn);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        //Resets Blocks Raycast to on to allow pointer to raycast the object
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeholder);
        
    }
}
