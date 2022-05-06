using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huds : MonoBehaviour
{
    [SerializeField] public hudLines lines;
    [SerializeField] public hudEditMap editmap;

    private void Awake()
    {
        core.huds = this;
    }
}
