using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public Data data;
    [SerializeField] private TMP_Text heartsText;

    private void Start()
    {
        data = new Data();
    }

    private void Update()
    {
        heartsText.text = data.hearts.ToString();
    }

    public void GenerateHearts()
    {
        data.hearts++;
    }
}
