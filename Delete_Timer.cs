using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Timer : MonoBehaviour
{
    public float lifeTime = 2.0f;
	
    void Start()
    {
        Destroy(this.gameObject , lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
