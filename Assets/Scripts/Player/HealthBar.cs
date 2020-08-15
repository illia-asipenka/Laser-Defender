using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerController player;
    private Image healthImage;
    
    // Start is called before the first frame update
    void Start()
    {
        healthImage = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = player.GetHpForUI();
    }
}
