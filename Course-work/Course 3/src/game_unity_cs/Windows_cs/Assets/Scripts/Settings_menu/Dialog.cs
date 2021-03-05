using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{

	[Header("Structure Settings")]
	public bool autoFindStructure;		// If activated it will fetch all necessary objects
	public GameObject imageTitle;		// Object responsible for the title of the dialog box
	public GameObject imageBody;		// Object responsible for the text of the dialog
	public GameObject imageOptions;		// Object responsible for the parent of the "yes" and "no"
	[Space]
	public Button yes;					// Button Component
	public Button no;					// Button Component

	[Header("Basic Settings")]
	public string dialogTitleText;		// Title text
	[TextArea]
	public string bodyText;				// Body text
	[Space]

	[Header("Button Events")]
	[SerializeField]
	private UnityEvent yesButtonEvents = new UnityEvent();	// Events of "Yes" Button

	[SerializeField]
	private UnityEvent noButtonEvents = new UnityEvent();	// Events of "No" Button

	// Variables that the user does not need to change
	private Text _dialogTextComponent;	// Title text component 
	private Text _bodyTextComponent;	// Body text component
	private Menu_controller _menuc;			// Menu Control Component

	void Start()
	{
		if (autoFindStructure == true)
		{
			autoFind();		// Calls the method responsible for finding all objects and components automatically
		}
		basicSettings();	// Method that defines the basic settings
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ClickedInNo();
		}
	}

	#region START METHODS DIALOG
	// Basic and necessary settings
	void basicSettings()
	{
		_menuc = FindObjectOfType<Menu_controller>();							// Get the Control Menu (there should only be one)
		_dialogTextComponent = imageTitle.GetComponentInChildren<Text>();	// Get title text component
		_bodyTextComponent = imageBody.GetComponentInChildren<Text>();		// Get body text component

		_dialogTextComponent.text = "" + dialogTitleText;					// Set title text
		_bodyTextComponent.text = "" + bodyText;							// Set body text

		setListeners();		// Call the listerners
	}

	// Method that looks for the structure of the dialog
	void autoFind()
	{
		imageTitle = this.transform.GetChild(0).gameObject;					// Take the first object from the parent "this"
		imageBody = this.transform.GetChild(1).gameObject;					// Take the second object from the parent "this"
		imageOptions = this.transform.GetChild(2).gameObject;				// Take the third object from the parent "this"

		yes = imageOptions.transform.GetChild(0).GetComponent<Button>();	// Take the first object from the parent "imageOptions"
		no = imageOptions.transform.GetChild(1).GetComponent<Button>();		// Take the second object from the parent "imageOptions"
	}
		
	void setListeners()							// Adds listeners (You can do it manually)
	{
		yes.onClick.AddListener(ClickedInYes);	// Add listener by clicking the button
		no.onClick.AddListener(ClickedInNo);	// Add listener by clicking the button
	}
	#endregion END METHODS DIALOG

	#region START METHODS ADD LISTENER
	// Listener for "YES" BUTTON
	void ClickedInYes()
	{
		yesButtonEvents.Invoke();	// Execute the event
	}

	// Listener for "NO" BUTTON
	void ClickedInNo() 
	{
		noButtonEvents.Invoke();	// Execute the event
	}
	#endregion END METHODS ADD LISTENER

	#region START METHODS YES FUNCTIONS
	public void quitGame()			// Method to exit the game / menu / program
	{
		Application.Quit();			// Quit
	}

	// Sample method only
	public void exampleMethod()
	{
		Debug.Log("MENUKIT: DO SOMETHING!");	// Show the message
	}
	#endregion END METHODS YES FUNCTIONS

	#region START METHODS NO FUNCTIONS
	// Method for the close dialog
	public void closeDialog()
	{
		this.gameObject.SetActive(false);		// Disables this object
		if (_menuc.inGame == false)	
			_menuc.mask.SetActive(false);		// Disables the mask object
	}
	#endregion END METHODS NO FUNCTIONS
}