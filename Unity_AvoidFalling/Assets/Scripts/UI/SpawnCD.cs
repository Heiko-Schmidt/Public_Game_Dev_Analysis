using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;

public class SpawnCD : MonoBehaviour
{
    [SerializeField] private int max_cd;
    private int cd;
    [SerializeField] AudioClip[] audio_clips;
    private AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = transform.GetComponent<AudioSource>();
        // register wave spawning as an event
        EventManager.register("SpawnWave", start_cd);
        EventManager.register("GameOver", game_over);
    }
    // FixedUpdate is called once per time step
    void FixedUpdate()
    {
        if(cd > 0)
        {
            transform.GetComponent<TextMeshProUGUI>().fontSize *= 1.02f;
        }
    }

    void game_over()
    {
        EventManager.unregister("SpawnWave", start_cd);
    }
    void start_cd()
    {
        cd = max_cd;
        transform.GetComponent<TextMeshProUGUI>().text = Convert.ToString(cd);
        audio_source.PlayOneShot(audio_clips[0], 0.3f);
        Invoke("run_cd", 1f);
    }
    void run_cd()
    {
        cd -= 1;
        transform.GetComponent<TextMeshProUGUI>().fontSize = 36f;
        if(cd > 0)
        {
            transform.GetComponent<TextMeshProUGUI>().text = Convert.ToString(cd);
            audio_source.PlayOneShot(audio_clips[0], 0.3f);
            Invoke("run_cd", 1f);
        }
        else if(cd == 0)
        {
            transform.GetComponent<TextMeshProUGUI>().fontSize = 60f;
            transform.GetComponent<TextMeshProUGUI>().text = "START";
            audio_source.PlayOneShot(audio_clips[1], 0.3f);
            Invoke("run_cd", 1f);
        }
        else
        {
            transform.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
