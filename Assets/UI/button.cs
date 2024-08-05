using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI thistxtl;
    AudioSource click;

    public void OnPointerEnter(PointerEventData eventData)
    {
        thistxtl.fontStyle = FontStyles.Bold;
        click.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        thistxtl.fontStyle = FontStyles.Normal;
    }

    void Start()
    {
        thistxtl = GetComponent<TextMeshProUGUI>();
        click = GetComponent<AudioSource>();
    }
}
