using UnityEngine;
using UnityEngine.UI;

public class GlobalUIButtonSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clickSound;

    void Start()
    {
        Button[] buttons = FindObjectsOfType<Button>(true);

        foreach (Button b in buttons)
        {
            b.onClick.AddListener(() => source.PlayOneShot(clickSound));
        }
    }
}