using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueStartController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index];
                gameObject.SetActive(false);
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        if (lines.Length != 0) {
            StartCoroutine(TypeLine());
        } else {
            gameObject.SetActive(false);
        }
        
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
