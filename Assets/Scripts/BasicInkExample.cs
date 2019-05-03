using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Ink.Runtime;
using System.Collections.Generic;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour
{

    private Button currentButton;

    [SerializeField]
    private TextAsset inkJSONAsset;
    private Story story;

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform selector;
    [SerializeField]
    private RawImage haze;


    // UI Prefabs
    [SerializeField]
    private Text textPrefab;
    [SerializeField]
    private Button buttonPrefab;

    List<Button> buttonList = new List<Button>();

    void Awake()
    {
        // Remove the default message
        //RemoveChildren();
        //StartStory();
    }


    public int GetChoice()
    {
        return story.currentChoices.Count;
    }

    public void SetSelector(int i)
    {
        if (i >= 0) {
            Vector3 newPos = buttonList[i].transform.position;
            newPos.x -= buttonList[i].GetComponent<RectTransform>().rect.xMax + 15;
            selector.position = newPos;
            //haze.GetComponent<RectTransform>().rect.xMax = buttonList[i].GetComponent<RectTransform>().rect.xMax + 25;
        }
    }

    public void ChangeSelectorView(bool value)
    {
        selector.gameObject.SetActive(value);
    }
    public void FinalizeSelect(int i)
    {
        if (i >= 0) { OnClickChoiceButton(story.currentChoices[i]); }
    }

    // Creates a new Story object with the compiled story which we can then play!
    public void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        RefreshView();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
        }
        buttonList.Clear();
        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });
                buttonList.Add(button);
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            Button choice = CreateChoiceView("You ignore the machine");
            choice.onClick.AddListener(delegate
            {
                RemoveChildren();
            });
            buttonList.Add(choice);
            MenuControl.MC.resumeActors();
        }
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Creates a button showing the choice text
    void CreateContentView(string text)
    {
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);
    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    public void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }
}
