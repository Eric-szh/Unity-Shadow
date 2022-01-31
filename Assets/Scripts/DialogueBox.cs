using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    bool triggered = false;
    public TextMeshProUGUI textComponent;
    public GameObject _someInter;
    public string[] lines;
    public float textSpeed;

    private int index;

    void Update()
    {
        if (triggered) {
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
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
            _someInter.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            index = 0;
            if (lines.Length != 0) {
                StartCoroutine(TypeLine());
                triggered = true;
                _someInter.SetActive(true);
                textComponent.text = string.Empty;
                triggered = true;
                StartDialogue();
            }
        }
        else
        {
            _someInter.SetActive(false);
        }
    }
}
