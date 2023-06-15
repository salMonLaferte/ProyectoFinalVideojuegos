using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Dialog : MonoBehaviour
{
    string[] dialog;
    
    int count = 0;

    [SerializeField]
    AudioClip nextTextSound;


    public void SetUp(string dialog)
    {
        this.dialog = dialog.Split("\n");
        Time.timeScale = 0;
        UpdateText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextText();
        }
    }

    void NextText()
    {
        count++;
        if(count >= dialog.Length)
        {
            Time.timeScale = 1;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                Player p = player.GetComponent<Player>();
                if (p.GetComponent<Player>() != null)
                {
                    p.ChangeToIddle();
                }
            }
            GameObject.Destroy(gameObject);
        }
        else{
            UpdateText();
        }
    }

    void UpdateText()
    {
        GetComponent<AudioSource>().Play();
        TMPro.TextMeshProUGUI tcomponent = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        tcomponent.text = dialog[count];
    }
}
