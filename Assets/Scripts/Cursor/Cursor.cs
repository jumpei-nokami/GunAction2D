using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public SelectSystem selectSystem;
    private RectTransform cursorTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        cursorTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        cursorTransform.anchoredPosition = (selectSystem.SelectStts == 0) ? new Vector2(-140, -60) : new Vector2(-140, -140);
    }
}
