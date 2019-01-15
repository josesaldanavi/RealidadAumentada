using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator player;
    private void OnMouseDown()
    {
        player.SetTrigger("Salto");
    }
}
