using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DecoyUI : MonoBehaviour
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

        if (Mathf.Approximately(GameState.Instance.DecoyCooldown, 0))
        {
            text.text = $"DECOY: READY";
        }
        else
        {
            int cd = (int)GameState.Instance.DecoyCooldown;
            text.text = $"DECOY: {cd}";
        }

    }
}
