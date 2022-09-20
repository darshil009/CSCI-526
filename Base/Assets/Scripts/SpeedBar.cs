using UnityEngine;
using UnityEngine.UI;

public class SpeedBar : MonoBehaviour
{

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = PlayerScript.playerSpeed;
    }
}
