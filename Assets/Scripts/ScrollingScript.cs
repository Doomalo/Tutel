using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
    private List<SpriteRenderer> backgroundPart;

    void Start()
    {
        backgroundPart = new List<SpriteRenderer>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SpriteRenderer r = child.GetComponent<SpriteRenderer>();
            if (r != null)
            {
                backgroundPart.Add(r);
            }
        }
        backgroundPart = backgroundPart.OrderBy(
          t => t.transform.position.x
           ).ToList();
    }

    void Update()
    {
        SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

        if (firstChild != null)
        {
            if (firstChild.bounds.max.x < Camera.main.transform.position.x - 45)
            {
                SpriteRenderer lastChild = backgroundPart.LastOrDefault();

                Vector3 lastPosition = lastChild.transform.position;
                Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

                firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x - 2, firstChild.transform.position.y, firstChild.transform.position.z);

                backgroundPart.Remove(firstChild);
                backgroundPart.Add(firstChild);
            }
        }
    }
}