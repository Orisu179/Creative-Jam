using System;
using System.Collections.Generic;
using UnityEngine;
using Event = AK.Wwise.Event;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public enum SoundEvent
    {
        PlayDialogue,
        PlayDrinkReval,
        PlayMenuButton,
        PlayPageFlip,
        PlayShake,
        PlaySteam,
        PlayTimeTravel,
        PlayLoopZeroMusic,
        PlayRewindMusic,
        StopLoopZeroMusic,
        StopRewindMusic
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Clear duplicates
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject); 
        
        _events = new Dictionary<SoundEvent, Event>
        {
            { SoundEvent.PlayDialogue, events[0] },
            { SoundEvent.PlayDrinkReval, events[1] },
            { SoundEvent.PlayLoopZeroMusic, events[2] },
            { SoundEvent.PlayMenuButton, events[3] },
            { SoundEvent.PlayPageFlip, events[4] },
            { SoundEvent.PlayShake, events[5] },
            { SoundEvent.PlaySteam, events[6] },
            { SoundEvent.PlayTimeTravel, events[7] },
            { SoundEvent.PlayRewindMusic , events[8] },
            { SoundEvent.StopLoopZeroMusic , events[9] },
            { SoundEvent.StopRewindMusic , events[10] }
        };
    }
    
    [SerializeField] private List<Event> events;
    private Dictionary<SoundEvent, Event> _events;

    public void Start()
    {
    }

    public void PostEvent(SoundEvent soundEvent)
    {
        _events[soundEvent].Post(gameObject);
    }
}
