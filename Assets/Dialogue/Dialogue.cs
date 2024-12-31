using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class Dialogue : MonoBehaviour
{
    private TextMeshProUGUI[] textComponents;
    private AudioSource soundEffectSource;
    //Below 3 properties deal with hiding and showing the text box
    private CanvasGroup canvasGroup;
    private readonly float lerpPauses = 0.01f;
    private readonly float incrementAmount = 0.01f;
    private bool proceed = false;
    private InputAction skipDialogueAction;


    public CharacterData characterData;
    void Start()
    {
        textComponents = GetComponentsInChildren<TextMeshProUGUI>();
        soundEffectSource = GetComponentInChildren<AudioSource>();
        textComponents[0].text = string.Empty;
        textComponents[1].text = string.Empty;
        canvasGroup = GetComponent<CanvasGroup>();
        skipDialogueAction = InputSystem.actions.FindAction("Submit");

        //THIS SECTION IS FOR TESTING
        string[] testLines = { "a", "asdf", "waefwaef", " afwe fanwj" };
        StartCoroutine(PlayDialogue(testLines, characterData));
    }
    void Update()
    {
        if (skipDialogueAction.WasPressedThisFrame())
        {
            proceed = true;
        }
    }
    /// <summary>
    /// Loads a character and plays dialogue.
    /// </summary>
    /// <param name="lines">Lines of Dialogue said by the character. Each line is a different screen of dialogue.</param>
    /// <param name="data">Data for the character that is speaking.</param>
    public IEnumerator PlayDialogue(string[] lines, CharacterData data)
    {
        LoadCharacter(data);
        yield return Show();
        foreach (string s in lines)
        {
            yield return TypeLine(s);
            proceed = false;
            yield return new WaitUntil(() => proceed);
            proceed = false;
        }
        yield return Hide();
    }


    void LoadCharacter(CharacterData data)
    {
        characterData = data;
        textComponents[0].text = data.name;
        GetComponentsInChildren<Image>()[1].sprite = characterData.sprite;
        textComponents[1].text = string.Empty;
        soundEffectSource.resource = characterData.soundEffect;
    }
    IEnumerator Hide()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        proceed = false;
        while (canvasGroup.alpha > 0 && !proceed)
        {
            canvasGroup.alpha -= incrementAmount;
            yield return new WaitForSeconds(lerpPauses);
        }
        proceed = false;
        canvasGroup.alpha = 0;
    }
    IEnumerator Show()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        proceed = false;
        while (canvasGroup.alpha < 1 && !proceed)
        {
            canvasGroup.alpha += incrementAmount;
            yield return new WaitForSeconds(lerpPauses);
        }
        proceed = false;
        canvasGroup.alpha = 1;
    }

    IEnumerator TypeLine(string s)
    {
        proceed = false;
        textComponents[1].text = string.Empty;
        foreach (char c in s.ToCharArray())
        {
            textComponents[1].text += c;
            if (!proceed)
            {
                if (char.IsLetterOrDigit(c))
                    soundEffectSource.Play();
                yield return new WaitForSeconds(characterData.textSpeed);
            }
        }
        proceed = false;
    }
}
