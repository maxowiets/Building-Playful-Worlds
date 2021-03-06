using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public PickUpType pickUpType;
    public MeshFilter model;
    public Dictionary<PickUpType, Mesh> modelDic = new Dictionary<PickUpType, Mesh>();

    public PickUpType[] allTypes;
    public Mesh[] allMesh;

    private void Awake()
    {
        for (int i = 0; i < allTypes.Length; i++)
        {
            modelDic.Add(allTypes[i], allMesh[i]);
        }

        var rand = Random.Range(0, allTypes.Length);
        pickUpType = allTypes[rand];
    }

    private void Start()
    {
        model.mesh = modelDic[pickUpType];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IncreaseStat();
            Destroy(this.gameObject);
        }
    }

    void IncreaseStat()
    {
        PlayerStats.totalBuffs++;

        switch (pickUpType)
        {
            case PickUpType.DAMAGE:
                PlayerStats.DamageMultiplier += 0.1f;
                break;
            case PickUpType.ATTACKSPEED:
                PlayerStats.AttackSpeedMultiplier += 0.1f;
                break;
            case PickUpType.RECOIL:
                PlayerStats.RecoilMultiplier *= 0.9f;
                break;
            case PickUpType.ACCURACY:
                PlayerStats.AccuracyMultiplier *= 0.9f;
                break;
            case PickUpType.RELOADSPEED:
                PlayerStats.ReloadSpeedMultiplier *= 0.9f;
                break;
            case PickUpType.CHARGESPEED:
                PlayerStats.ChargeSpeedMultiplier += 0.1f;
                break;
            case PickUpType.MOVEMENTSPEED:
                PlayerStats.SpeedMultiplier += 0.1f;
                break;
            case PickUpType.SPRINTSPEED:
                PlayerStats.SprintSpeedMultiplier += 0.1f;
                break;
            case PickUpType.JUMPHEIGHT:
                PlayerStats.JumpHeightMultiplier += 0.1f;
                break;
            default:
                break;
        }

        UIManager.Instance.buffUI.AddBuffUI(pickUpType);
    }
}
