using System;
using Ink.Runtime;
using UnityEngine;

public class InkExternalFunctions 
{
      public void Bind(Story story)
    {
        story.BindExternalFunction("background", (string imageName) => SetBackground(imageName));

    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("background");
    }

    private static void SetBackground(string imageName)
    {
        DialogueManager dm = DialogueManager.GetInstance();
        if (dm == null)
        {
            Debug.LogWarning("DialogueManager not found.");
            return;
        }

        if (dm.backgroundImage == null)
        {
            Debug.LogWarning("DialogueManager.background not assigned.");
            return;
        }

        SpriteRenderer sr = dm.backgroundImage.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.Log("No SpriteRenderer.");
            return;
        }

        Sprite target = FindSpriteByName(dm.bgSprite, imageName);
        if (target == null)
        {
            Debug.Log("No sprite found");
            return;
        }

        sr.sprite = target;
    }

    private static Sprite FindSpriteByName(Sprite[] sprites, string name)
    {
        if (sprites == null) return null;

        for (int i = 0; i < sprites.Length; i++)
        {
            var s = sprites[i];
            if (s != null && s.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                return s;
        }

        return null;
    }
}
