using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ComicText : MonoBehaviour
{
    public float zRotation;
    public float amountToRotate;

    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float randOffset;

    public TextMeshProUGUI text;

    public float leftMovementSpeed;
    public float leftAcceleration;

    public void Init(string newText, Transform spawnPosition)
    {
        text.SetText(newText);

        text.transform.eulerAngles = new Vector3(0, 0, Random.Range(-zRotation, zRotation));

        transform.position = new Vector3(spawnPosition.position.x + xOffset, spawnPosition.position.y, zOffset);

        float randX = Random.Range(-randOffset, randOffset);
        float randY = Random.Range(-randOffset, randOffset);
        Vector3 randVector2Offset = new Vector3(randX, randY, 0);

        transform.position += randVector2Offset;

        transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 0.25f, 10, 1);

        Vector3 newRotate = new Vector3(0, 0, Random.Range(-amountToRotate, amountToRotate));
        text.transform.DORotate(newRotate, 2, RotateMode.Fast).SetEase(Ease.OutQuad);

        if (spawnPosition.position.y < 3.5f)
        {
            text.transform.DOMoveY(transform.position.y + yOffset, .25f).SetEase(Ease.OutQuad);
        }

        StartCoroutine(FadeCo());

        //float xPosToMoveTo = transform.position.x - 1f + Random.Range(-.1f, .1f);

        //transform.DOMoveX(xPosToMoveTo, 3, false).SetEase(Ease.InQuad);

        Destroy(gameObject, 3);
    }

    private IEnumerator FadeCo()
    {
        yield return new WaitForSeconds(1);
        text.DOFade(0, 2);
    }

    private void FixedUpdate()
    {
        leftMovementSpeed *= leftAcceleration;
        transform.Translate(Vector3.left * leftMovementSpeed);
        //transform.position -= Vector3.left * leftMovementSpeed;
    }
}
