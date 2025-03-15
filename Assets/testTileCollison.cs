using System;
using System.Collections;
using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class testTileCollison : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        other.GetContact(0).point.LogSelf(other.gameObject);
    }
    [Button]
    public void TestIsUsing()
    {
        var t = new childclass();
        (t is baseclass).LogSelf();
    }

    class baseclass
    {
        
    }

    class childclass : baseclass
    {
        
    }
}
