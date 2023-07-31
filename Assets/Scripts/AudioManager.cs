using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;


//Here we are storing Sounds, and creating function to play and adjust Sounds from other scripts.
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    //here we will loop thru the list of Sounds and add a source to each Sounds
    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("menu");
    }

    //function to play Sounds
    public void Play(string name)
    {
        //not 100% on the syntax, but this loops thru the sounds function to find a Sounds such that sound.name = name
        Sound s = Array.Find(sounds, sounds => sounds.name == name);

        //if the sound by name wasn't found, exit the function(so it doesnt play an empty sound and throw and error
        if (s == null)
        {
            Debug.LogWarning(name + " aint here bud");
            return;
        }


        s.source.Play();
    }

    public void Stop(string name)
    {
        //still not 100% on the syntax, but this loops thru the sounds function to find a Sounds such that sound.name = name
        Sound s = Array.Find(sounds, sounds => sounds.name == name);

        //if the sound by name wasn't found, exit the function(so it doesnt play an empty sound and throw and error
        if (s == null)
        {
            Debug.LogWarning(name + " aint here bud");
            return;
        }

        s.source.Stop();
    }

    public Sound FindSound(string name)
    {
        //still not 100% on the syntax, but this loops thru the sounds function to find a Sounds such that sound.name = name
        Sound s = Array.Find(sounds, sounds => sounds.name == name);

        //if the sound by name wasn't found, exit the function(so it doesnt play an empty sound and throw and error
        if (s == null)
        {
            Debug.LogWarning(name + " aint here bud");
            return null;
        }
        return s;

    }

    public void RoundStart()
    {
        Debug.Log("game start");
        if (FindSound("menu").source.isPlaying)
            Stop("menu");

        if (FindSound("stop").source.isPlaying)
            Stop("stop");

        Play("18loopintro");
    }

    public void Stop()
    {
        Play("stop");
    }

    public void Reset()
    {
        foreach (Sound s in sounds)
            if (s.source.isPlaying)
                s.source.Stop();

        Play("18loopintro");
    }

    public void HardReset()
    {
        foreach (Sound s in sounds)
            if (s.source.isPlaying)
                s.source.Stop();

        Play("menu");
    }


    public void CrowdCheer()
    {
        if (FindSound("cheer1").source.isPlaying ||
            FindSound("cheer2").source.isPlaying)
            return;

        string n = UnityEngine.Random.Range(1, 2).ToString();
        Sound s = Array.Find(sounds, sounds => sounds.name == "cheer" + n);

        if (s == null)
        {
            Debug.LogWarning(name + "aint here bud");
            return;
        }
        s.loop = false;
        s.source.Play();
    }

    public void CrowdLaugh()
    {
        if (FindSound("laugh1").source.isPlaying ||
            FindSound("laugh2").source.isPlaying)
            return;

        string n = UnityEngine.Random.Range(1, 2).ToString();
        Sound s = Array.Find(sounds, sounds => sounds.name == "laugh" + n);

        if (s == null)
        {
            Debug.LogWarning(name + "aint here bud");
            return;
        }
        s.loop = false;
        s.source.Play();
    }
}
