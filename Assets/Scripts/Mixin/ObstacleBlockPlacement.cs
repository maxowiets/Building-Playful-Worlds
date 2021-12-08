using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBlockPlacement : MixinBase
{
    public Transform cam;
    public GameObject obstacle;
    Vector3 placementPosition;
    bool placeable;
    public FloatData data;

    RaycastHit hit;
    public float placementRange;

    public GameObject obstacleIndicator;
    MeshRenderer meshRen;

    private void Awake()
    {
        meshRen = obstacleIndicator.GetComponent<MeshRenderer>();
    }

    public override bool Check()
    {
        return placeable;
    }

    public override void Action()
    {
        Instantiate(obstacle, placementPosition, Quaternion.identity);
    }

    private void Update()
    {
        Physics.Raycast(cam.position, cam.forward, out hit, placementRange);

        if (hit.collider != null)
        {
            var xCal = Mathf.Floor(hit.point.x / 2) * 2 + 1;
            var yCal = Mathf.Floor(hit.point.y / 2) * 2 + 1;
            var zCal = Mathf.Floor(hit.point.z / 2) * 2 + 1;
            placementPosition = new Vector3(xCal, yCal, zCal);

            if (obstacleIndicator.activeSelf == false)
            {
                obstacleIndicator.SetActive(true);
            }
            obstacleIndicator.transform.position = placementPosition;
            obstacleIndicator.transform.rotation = Quaternion.Euler(0, 0, 0);


            //calculate if nothing is inside the box you want to place
            Collider[] col = Physics.OverlapBox(placementPosition, Vector3.one * 0.9f, Quaternion.Euler(0, 0, 0));
            if (col.Length != 0 || data.CurrentClipSize <= 0)
            {
                meshRen.material.color = new Color(1, 0, 0, 0.4f);
                placeable = false;
            }
            else
            {
                meshRen.material.color = new Color(0, 1, 1, 0.4f);
                placeable = true;
            }
        }

        if (hit.collider == null)
        {
            if (obstacleIndicator.activeSelf == true)
            {
                obstacleIndicator.SetActive(false);
            }
            placeable = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cam.position, cam.position + cam.forward * 5);
    }
}
