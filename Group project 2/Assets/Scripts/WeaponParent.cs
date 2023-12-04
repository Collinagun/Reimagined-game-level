using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 Pointerposition { get; set; }
    public Vector2 direction { get; set; }

    public void Update()
    {
        // Will set the value of the variable to the current position of the mouse to be used in the player code
        // with help from the Unity Input Action Manager
        direction = (Pointerposition - (Vector2)transform.position).normalized;
        transform.right = direction;

        // Will make sure that the weapon will be rotating upright
        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }

        transform.localScale = scale;

        // Will make sure the shotgun would be overlapping the player sprite when aimed under the torso
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector2(-1f, 1f);
            // transform.rotation = Quaternion.Euler(0, 0, 0); // Aim to the left
        }
        // Move right
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector2(1f, 1f);
            // transform.rotation = Quaternion.Euler(0, 0, 0); // Aim to the right
        }
       }
    }
