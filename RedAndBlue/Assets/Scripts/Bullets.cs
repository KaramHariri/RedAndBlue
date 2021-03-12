using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    float speed = 25.0f;
    public enum teamColor
    {
        Red,
        Blue
    }

    private teamColor color = teamColor.Red;

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    public void SetTeamColor(teamColor newColor)
    {
        color = newColor;
        var renderer = GetComponent<Renderer>();

        switch (color)
        {
            case teamColor.Red:
            {
                renderer.material.SetColor("_Color", Color.red);
            }
                break;
            case teamColor.Blue:
            {
                renderer.material.SetColor("_Color", Color.blue);
            }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        
    }
}
