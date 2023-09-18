using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dolgu : MonoBehaviour
{
    [SerializeField] private float dlg = 15.5f;
    [SerializeField] private float ElemanBoyutu = 100.0f;
    [SerializeField] private float istenilenAralik = 250.0f;

    [SerializeField] private RectTransform rt;
    private int elemanMik;
    private float IcerikBoyutu;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        elemanMik = rt.childCount;
        IcerikBoyutu = ((elemanMik * (ElemanBoyutu + dlg)) - istenilenAralik) * rt.localScale.x;

        if (rt.localPosition.x > dlg)
            rt.localPosition = new Vector3(dlg, rt.localPosition.y, rt.localPosition.z);
        else if (rt.localPosition.x < -IcerikBoyutu)
            rt.localPosition = new Vector3(-IcerikBoyutu, rt.localPosition.y, rt.localPosition.z);
    }

}
