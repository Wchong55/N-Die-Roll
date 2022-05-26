using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Rigidbody rb;

    public bool jump = false;
    public bool falling = true;

    //public GameObject interaction;
    //private ARTapToPlaceObject script;

    public float initPos;
    public static Vector3 diceVel;

    public float time = 0;

    // Start is called before the first frame update
    void Awake()
    {
        rb.useGravity = true;
        rb.isKinematic = false;

        //script = interaction.GetComponent<ARTapToPlaceObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jump) {
            jump = false;
            rb.AddForce(new Vector3(0, UnityEngine.Random.Range(75, 200), 0), ForceMode.Acceleration);
            rb.AddTorque(UnityEngine.Random.Range(0, 500), UnityEngine.Random.Range(0, 500), UnityEngine.Random.Range(0, 500));
            //jump = false;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Plane")
        {
            falling = false;
        }
    }
}
