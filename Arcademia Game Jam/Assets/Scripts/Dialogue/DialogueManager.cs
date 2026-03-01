using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SearchService;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJSON;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    [SerializeField] private GameObject choicedPanel;

    [Header("Backgrounds")]
    public Image backgroundImage;
    public Sprite[] bgSprite;

    [Header("Canvas")]
    public GameObject dialogueCanvas;

    [Header("Tags")]
    private const string SPEAKER_TAG = "speaker";
    private const string BG_TAG = "bg";

    private Story currentStory;
    public bool dialogueIsPlaying {  get; private set; }

    private static DialogueManager instance;
    private InkExternalFunctions inkExternalFunctions;

    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("duplicate DialogueManagers");
        }
        instance = this;
        
        inkExternalFunctions = new InkExternalFunctions();
       // dialogueVariables = new DialogueVariables(loadGlobalsJSON);

        choicesText = new TextMeshProUGUI[choices.Length];

        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
        if (continueButton != null )
        continueButton.onClick.AddListener(ContinueStory);
    }

    internal static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        HideChoices();
        StartDialogue();

    }


    public void StartDialogue()
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        inkExternalFunctions.Bind(currentStory);
        ContinueStory();
    }

    private void ContinueStory()
    {
        if (!dialogueIsPlaying)
        {
            return ;
        }

        if (currentStory.canContinue)
        {
            string line = currentStory.Continue();
            dialogueText.text = line;

           HandleTags(currentStory.currentTags);

            if (!dialoguePanel.activeSelf)
                return;

            if (currentStory.currentChoices.Count > 0)
            {
                DisplayChoices();
                continueButton.gameObject.SetActive(false);
            }
            else
            {
                HideChoices();
                continueButton.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }

        }

        else
        {
            ExitDialogue();
        }
    }

    private void DisplayChoices()
    {
        
        for (int i = 0; i < currentStory.currentChoices.Count; i++)
        {
            choices[i].SetActive(true);
            choicesText[i].text = currentStory.currentChoices[i].text;
           
        }
        choicedPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void PauseDialogue()
    {
        dialoguePanel.SetActive(false);
        choicedPanel.SetActive(false);
    }

    public void ResumeDialogue()
    {
        dialoguePanel.SetActive(true);
        
        ContinueStory();
    }

    public void SelectChoice(int index)
    {
        
        choicedPanel.SetActive(false);
        currentStory.ChooseChoiceIndex(index);
        //HideChoices();
        ContinueStory() ;
        //HideChoices();
    }

    private void HideChoices()
    {
        foreach (GameObject button in choices)
        {
            button.SetActive(false);
        }
    }

    private void ExitDialogue()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        displayNameText.text = "";

        HideChoices();
        currentStory = null;
    }

    private void HandleTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            string[] split = tag.Split(':');
            if (split.Length != 2) continue;

            string key = split[0].Trim().ToLower();
            string value = split[1].Trim();

            switch (key)
            {
                case "speaker":
                    displayNameText.text = value;
                    break;

                case "bg":
                    
                    ChangeBackground(value);
                    break;

                case "battle":
                    PauseDialogue();
                    overallscenemanager.SwitchtoBattleSystem(value, this);
                    break;
            }
        }
       
    }

    public OverallSceneManager overallscenemanager;

    private void ChangeBackground(string value)
    {
       if (backgroundImage == null)
        {
            Debug.Log("no bg");
            return;
        }

       if (bgSprite != null || bgSprite.Length == 0)
        {
            Debug.Log("bgSprites empty");
            return ;
        }

        for (int i = 0; i < bgSprite.Length; i++)
        {
            Sprite s = bgSprite[i];
            if (s != null && s.name.Equals(value, System.StringComparison.OrdinalIgnoreCase))
            {
                backgroundImage.sprite = s;
                return;
            }
        }
        Debug.Log(" bg sprite not found");
    }

}
