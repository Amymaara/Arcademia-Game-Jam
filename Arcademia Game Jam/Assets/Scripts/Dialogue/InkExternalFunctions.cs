using Ink.Runtime;
using UnityEngine;

public class InkExternalFunctions : MonoBehaviour
{
      public void Bind(Story story)
    {
        story.BindExternalFunction("background", (string backgroundname) => background(backgroundname));

    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("background");
    }

    public static void background(string imagename)
    {
        DialogueManager dialogueManager = DialogueManager.GetInstance();
        GameObject targetObject = dialogueManager.background;
        if (targetObject == null)
        {
            Debug.Log("Not assigned");
            return;
        }

        SpriteRenderer spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
        Sprite[] sprites = dialogueManager.bgSprite;
        foreach (Sprite sprite in sprites)
        {
            if (sprite.name == imagename)
            {
                spriteRenderer.sprite = sprite;
                break;
            }
        }
        
    }
}
