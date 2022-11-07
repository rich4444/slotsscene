using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot_item : MonoBehaviour
{
    public string id;
    public bool start = false;
    public Image img;
    public Sprite[] images;
    public Transform[] positions;
    public int nextPos;
    public float minDistance = 0.5f;
    public float movementSpeed = 0.5f;

    public int currentPos;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        int spriteNumber = (int)movementSpeed / 3;
        if (spriteNumber > images.Length)
            spriteNumber = images.Length - 1;
        if (spriteNumber < 1)
            spriteNumber = 1;
        img.sprite = images[spriteNumber - 1];

        if (start && movementSpeed < 15)
        {
            movementSpeed += 7.5f * Time.deltaTime;
        }
        else if (!start && movementSpeed >= 3)
        {
            movementSpeed -= 3 * Time.deltaTime;
        }
        else if (!start && movementSpeed >= 1)
        {
            movementSpeed -= 0.5f * Time.deltaTime;
        }

        if (movementSpeed <= 0)
        {
            movementSpeed = 0;
        }

        currentPos = GetCurrentPos();
    }

    void FixedUpdate()
    {
        if (start || movementSpeed > 1)
            MoveToTheNextPos();
        if (!start && movementSpeed <= 1)
            MoveToCloser();
    }

    void MoveToCloser()
    {
        float distNext = transform.position.y - positions[nextPos].position.y;
        float distPrev = positions[GetCurrentPos()].position.y - transform.position.y;

        int target = nextPos;
        int dir = -1;
        if (distPrev < distNext)
        {
            target = GetCurrentPos();
            dir = 1;
        }


        float dist = Mathf.Abs(transform.position.y - positions[target].position.y);
        if (dist <= minDistance * 2)
        {
            transform.position = positions[target].position;
            movementSpeed = 0;
        }

        Vector2 position = transform.position;

        position.y += movementSpeed * Time.deltaTime * dir * Mathf.Abs(positions[nextPos].position.y - positions[nextPos - 1].position.y);

        transform.position = position;

    }


    void MoveToTheNextPos()
    {
        Vector2 dist = transform.position - positions[nextPos].position;
        if (dist.y <= minDistance)
        {
            transform.position = positions[nextPos].position;
            nextPos++;
            if (nextPos > positions.Length - 1)
            {
                nextPos = 1;
                transform.position = positions[0].position;
            }
        }

        Vector2 position = transform.position;

        position.y += movementSpeed * Time.deltaTime * -1 * Mathf.Abs(positions[nextPos].position.y - positions[nextPos - 1].position.y);

        transform.position = position;
    }

    public int GetCurrentPos()
    {
        if (nextPos == 0) return positions.Length - 1;
        return nextPos - 1;
    }
}
