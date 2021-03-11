using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ActDisplay : MonoBehaviour{

    public Sprite empty;
    private Image actImage;

    void Start()
    {
        actImage = GetComponent<Image>();
    }
}
