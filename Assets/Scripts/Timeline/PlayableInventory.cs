using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayableInventory : MonoBehaviour
{
    public SerializableDictionary<string,TimelineAsset> TimelineAssets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryGetTimelineAsset(string assetName, out TimelineAsset timelineAsset)
    {
        return TimelineAssets.TryGetValue(assetName, out timelineAsset);
    }
}
