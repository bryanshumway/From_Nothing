using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_01_movement : MonoBehaviour
{

    public float speed;
    public float distance;

    private float currPos;
    private float newPos;
    private float currDir;
    private GameObject messagePanel;
    private Text messageText;
    private bool startPos = true;

    private void Start()
    {
        currPos = transform.position.x;
        newPos = transform.position.x + distance;
        currDir = transform.localScale.z;
        messagePanel = GameObject.Find("MessagePanel");
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
    }

    private void Update()
    {
        if (startPos)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, currDir);
            if (transform.position.x >= newPos)
            {
                startPos = false;
            }
        }
        else
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -currDir);
            if (transform.position.x <= currPos)
            {
                startPos = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && PlayerController.canJump == false)
        {
            messageText.text = "I need to find a way over this robot...";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageRemove");
            StartCoroutine("MessageRemove");
        }
    }

    IEnumerator MessageRemove()
    {
        yield return new WaitForSeconds(2);
        messageText.text = "";
        messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

}
