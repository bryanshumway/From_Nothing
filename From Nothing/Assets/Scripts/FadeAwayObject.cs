using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayObject : MonoBehaviour
{

    public float fadeStart;
    public float fadeSpeed;

    private float counter = 0;
    private bool fadeActive;
    private Color objectColor;
    private SpriteRenderer renderer;

    private void Start()
    {
        fadeActive = true;
        renderer = GetComponent<SpriteRenderer>();
        objectColor = renderer.color;
        StartCoroutine(FadeAway());
    }

    private void Update()
    {
        if (fadeActive)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / fadeSpeed);
            renderer.color = new Color(objectColor.r, objectColor.g, objectColor.b, alpha);
        }
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(fadeStart);
        fadeActive = true;
        yield return new WaitForSeconds(fadeSpeed);
        Destroy(gameObject);
    }

}
