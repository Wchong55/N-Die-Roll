using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public GameObject interaction;
    private ARTapToPlaceObject script;

    // Start is called before the first frame update
    void Start()
    {
        script = interaction.GetComponent<ARTapToPlaceObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rollDice()
    {
        foreach (GameObject die in script.dice) {
            die.GetComponent<Dice>().jump = true;
        }
    }
}
