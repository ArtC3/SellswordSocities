using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {
    public enum dropZoneTypes {TASKCHOICES = 0, TASKSCHEDULER = 1, GARBAGECOLLECT};
    public dropZoneTypes dZType;
	public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on to " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)//if what we "grabbed" exists we will check where we dropped it
        {
            switch (dZType) {
                case dropZoneTypes.TASKSCHEDULER:
                    if (d.parentToReturn == this.transform) break; //If we're dropping the draggable in the same location don't bother
                    //create a copy of the item dropped then reset its values and place it in the TaskChoices
                    GameObject temp = Instantiate(d.gameObject);
                    temp.transform.SetParent(d.parentToReturn);
                    temp.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    d.parentToReturn = this.transform;
                    break;
                case dropZoneTypes.TASKCHOICES:
                    if (d.parentToReturn == this.transform) break; //If we're dropping the draggable in the same location don't bother
                    Destroy(d.gameObject);//If an item from the scheduler is returned to choices it is destroyed
                    break;
                case dropZoneTypes.GARBAGECOLLECT:
                    //Tasks in TaskChoices must be industructable
                    if (d.parentToReturn.gameObject.GetComponent<DropZone>().dZType == dropZoneTypes.TASKCHOICES)
                        break;
                    Destroy(d.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
