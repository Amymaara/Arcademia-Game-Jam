using TMPro;
using UnityEngine;
using Ink.Runtime;
using System;
using System.Collections.Generic;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJSON;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Backgrounds")]
    public GameObject background;
    public Sprite[] bgSprite;

    [Header("Canvas")]
    public GameObject dialogueCanvas;

    [Header("Tags")]
    private const string SPEAKER_TAG = "speaker";

    private Story currentStory;
    public bool dialogueIsPlaying {  get; private set; }
    private bool canContinue = false;

    private static DialogueManager instance;
    private InkExternalFunctions inkExternalFunctions;
    // private DialogueVariables dialogueVariables;

    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("duplicate DialogueManagers");
        }
        instance = this;
        
        inkExternalFunctions = new InkExternalFunctions();
       // dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    internal static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
      if (!dialogueIsPlaying)
        {
            return;
        }

      if (canContinue && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }

    public void EnterDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

      //  dialogueVariables.StartListening(currentStory);
        inkExternalFunctions.Bind(currentStory);
        ContinueStory();
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(0.2f);

       // dialogueVariables.StopListening(currentStory);
       inkExternalFunctions.Unbind(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    private void ContinueStory()
    {

    }
}
