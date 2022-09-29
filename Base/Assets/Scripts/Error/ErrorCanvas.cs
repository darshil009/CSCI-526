using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject errorText;
    private float timeToShow = 3f;
    private string text;
    void Start()
    {
        errorText = transform.Find("ErrorText").gameObject;
        errorText.SetActive(false);
        
    }

    public void ShowError(string text)
    {
        this.text = text;
        StopCoroutine(ShowErrorCoroutine());
        StartCoroutine(ShowErrorCoroutine());
    }

    IEnumerator ShowErrorCoroutine()
    {
        float time = 0;
        errorText.SetActive(true);
        while(time<timeToShow)
        {
            yield return new WaitForSeconds(1);
            time++;
        }
        errorText.SetActive(false);
        yield break;
    }


}
