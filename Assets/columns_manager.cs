using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class columns_manager : MonoBehaviour
{
    public GameObject[] columns;
    public bool start = false;

    void Update()
    {
        for (int i = 0; i < columns.Length; i++)
        {
            StartCoroutine(ChangeStart(columns[i], start, i));
        }
    }

    public void ChangeValue(bool v)
    {
        start = v;
    }


    IEnumerator ChangeStart(GameObject i, bool s, int t = 0)
    {
        float r = Random.Range(1.2f, 1.6f);
        if (!s) r = 0;
        yield return new WaitForSeconds(t * r);
        slot_item[] slots = i.GetComponentsInChildren<slot_item>();
        foreach (slot_item j in slots)
        {
            j.start = s;
        }
    }
}

