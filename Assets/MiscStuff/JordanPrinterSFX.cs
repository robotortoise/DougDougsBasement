using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class JordanPrinterSFX : MonoBehaviour
{
    public bool RevealFinished {get; set;}

    public UnityEvent customEvent;

    void Start() {
        RevealFinished = false;
    }

    void Update() {
        if(Input.anyKeyDown) {
            customEvent.Invoke();
            enabled = false;
        }
    }
}
