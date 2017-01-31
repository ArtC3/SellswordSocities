using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTabsManager : MonoBehaviour {
    [SerializeField]
    public bool isScheduler;
    public List<Button> ButtonGrouping = new List<Button>();
    public List<GameObject> PanelGrouping = new List<GameObject>();
    public GameObject currPanel;
    public Button currButton;

    // Use this for initialization
    void Start () {
        //find every button or panel under the managing list
		foreach (Transform _child in transform)
        {
            if(_child.gameObject.GetComponent<Button>() != null)
                ButtonGrouping.Add(_child.gameObject.GetComponent<Button>());
            else
                PanelGrouping.Add(_child.gameObject);
        }

        ResetButtonsAndPanels();

        //sets the first button and panel to a selected point
        ButtonGrouping[0].interactable = false;
        currButton = ButtonGrouping[0];
        PanelGrouping[0].SetActive(true);
        currPanel = PanelGrouping[0];
	}

    //switch the button and the panel it is associated with
    public void SwitchPanels(Button _btn)
    {
        ResetButtonsAndPanels();
        //set the associated panel to active and keep the button "pressed"
        _btn.gameObject.GetComponent<TabScript>().associatedPanel.SetActive(true);
        currPanel = _btn.gameObject.GetComponent<TabScript>().associatedPanel;
        _btn.interactable = false;
        currButton = _btn;
    }
    
    
    //resets all buttons and panels to "off" state
    public void ResetButtonsAndPanels()
    {
        foreach (Button btn in ButtonGrouping)
        {
            btn.interactable = true;
        }
        foreach (GameObject panel in PanelGrouping)
        {
            panel.SetActive(false);
        }
    }
}
