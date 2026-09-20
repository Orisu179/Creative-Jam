using System;
using System.Collections.Generic;
using UnityEngine;
using Event = AK.Wwise.Event;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public enum SoundEvent
    {
        PlayIngredient1,
        PlayIngredient2,
        PlayMusic,
        StopMusic
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
    }
    
    [SerializeField] private List<Event> startEvents;
    [SerializeField] private List<Event> endEvents;
    private Dictionary<SoundEvent, Event> _startEvents;
    private Dictionary<SoundEvent, Event> _endEvents;

    public void Start()
    {
        _startEvents = new Dictionary<SoundEvent, Event>
        {
            { SoundEvent.PlayMusic, startEvents[0] },
            { SoundEvent.PlayIngredient1, startEvents[1] },
            { SoundEvent.PlayIngredient2, startEvents[2] },
        };
        _endEvents = new Dictionary<SoundEvent, Event>
        {
            { SoundEvent.StopMusic, endEvents[0] },
        };
    }

    public void PostEvent(SoundEvent soundEvent, bool start)
    {
        if (start)
        {
            _startEvents[soundEvent].Post(gameObject);
        }
        else
        {
            _endEvents[soundEvent].Post(gameObject);
        }
    }
    
}
