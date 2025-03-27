using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestImpluse : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Button]
    public void GenerateImpluse(Vector3 vel)
    {
        impulseSource.GenerateImpulse(vel);
    }
}
