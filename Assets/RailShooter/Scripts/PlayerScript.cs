using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.collider != null)
                {
                    IHitable[] hitables = hit.collider.GetComponents<IHitable>();

                    if (hitables != null && hitables.Length > 0)
                    {
                        foreach (var hitable in hitables)
                        {
                            hitable.Hit(hit);
                        }
                    }

                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }
}
