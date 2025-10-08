using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusController : MonoBehaviour
{
    public GameObject primul;
    public GameObject aldoilea;
    public float distanta = 0.25f;
    float dist = 100;

    private Animator animator1;
    private Animator animator2;

    // Start is called before the first frame update
    void Start()
    {
        animator1 = primul.GetComponentInChildren<Animator>();
        animator2 = aldoilea.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (primul == null || aldoilea == null) return; //nu avem nmk YET


        dist = Vector3.Distance(primul.transform.position, aldoilea.transform.position);
        Debug.Log("Distanta este " + dist);

        if (dist <= distanta)
        {
            animator1.SetTrigger("SaAtacamTS");
            animator2.SetTrigger("SaAtacamTS");
        }
    }
}
