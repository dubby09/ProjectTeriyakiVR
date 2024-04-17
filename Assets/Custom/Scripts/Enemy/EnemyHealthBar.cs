using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// source: https://www.youtube.com/watch?v=_lREXfAMUcE
public class EnemyHealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Image fillImage;
    //[SerializeField] private Transform target;

    public void UpdateHealthBar(SimpleEnemy enemy)
    {
        slider.value = enemy.health / enemy.maxHealth;
    }

    private void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // hides the fill of the slider when the value is 0
        // sliders arent completely empty when their values are zero, unity is so silly!!
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        else
        {
            fillImage.enabled = true;
        }

        // roates the health bar in sync with the player camera so that it always appears
        // flat to the camera
        transform.rotation = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y, 0f);

        // for non-VR 3rd person camera:
        //transform.rotation = playerCamera.transform.rotation;

    }
}
