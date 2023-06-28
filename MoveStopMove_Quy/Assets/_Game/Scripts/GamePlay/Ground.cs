using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Bounds bounds;

    private void Start()
    {
        player = GameObject.Find("Player");
        bounds.center = transform.position;
        bounds.extents = new Vector3(35f, 0, 35f);
    }

    private void Update()
    {
        LimitArea();
    }
    public void LimitArea()
    {
        if (player.transform.position.x >= bounds.max.x)
        {
            player.transform.position = new Vector3(bounds.max.x, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.z >= bounds.max.z)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, bounds.max.z);
        }
        if (player.transform.position.x <= bounds.min.x)
        {
            player.transform.position = new Vector3(bounds.min.x, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.z <= bounds.min.z)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, bounds.min.z);
        }

    }
}
