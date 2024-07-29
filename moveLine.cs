using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.UI;

public class moveLine : MonoBehaviour
{
    private float movespeedup = 20f;
    public GameObject MoveGrat;
    public float RangeHE;
    RectTransform rectTransform;
    public float gratmovingValue;



    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        gratmovingValue = rectTransform.localPosition.y;

        
        if (Input.GetKey(KeyCode.L))
            MoveGrat.transform.Translate(Vector3.up * movespeedup * Time.deltaTime);

        if (Input.GetKey(KeyCode.P))
            MoveGrat.transform.Translate(-Vector3.up * movespeedup * Time.deltaTime);

        // Calculation for HE Ammo
        RangeHE = ((5000 * gratmovingValue )/ 238) - 231.1f;
    }
}
