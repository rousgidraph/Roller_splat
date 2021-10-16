using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_piece : MonoBehaviour
{
    public bool is_coloured = false;


    public void ChangeColour(Color color) {
        GetComponent<MeshRenderer>().material.color = color;
        is_coloured = true;
        gamemanager.singleton.checkCompleted();
    }

}
