using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HackUI : MonoBehaviour
{
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Mathf.Approximately(GameState.Instance.HackCooldown, 0) )
        {
            text.text = $"HACK: READY";
        } else
        {
            int cd = (int)GameState.Instance.HackCooldown;
            text.text = $"HACK: {cd}";
        }
        
    }
}
