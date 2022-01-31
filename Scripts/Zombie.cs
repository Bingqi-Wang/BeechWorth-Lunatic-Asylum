using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Game;
using Opsive.UltimateCharacterController.Character.Abilities;
public class Zombie : MonoBehaviour
{
    protected NavMeshAgent nav;

    protected Transform playerTrans;

    protected bool startMoving;

    public float attackDistance;

    protected bool isMoving;

    protected UltimateCharacterLocomotion characterLocomotion;

    protected Use use;

    public GameObject bloodEffect;

    protected bool hasDead;

    private Ability[] abilities;

    // Start is called before the first frame update
    protected void Start()
    {
        nav = transform.GetComponent<NavMeshAgent>();
        characterLocomotion = GetComponent<UltimateCharacterLocomotion>();
        use = characterLocomotion.GetAbility<Use>();
        abilities = characterLocomotion.GetAbilities<Ability>();
        //abilities[4].Enabled = false;
        Invoke("GetMoveAbility", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDead)
        {
            return;
        }

        if (startMoving)
        {
            if (isMoving)//移动
            {
                if (Vector3.Distance(playerTrans.position, nav.transform.position) > attackDistance)
                {
                    //未到达攻击距离
                    nav.SetDestination(playerTrans.position);
                }
                else
                {
                    //已到达攻击距离
                    nav.isStopped = true;
                    isMoving = false;
                }
            }
            else//攻击
            {
                //重新移动
                if (Vector3.Distance(playerTrans.position, nav.transform.position) > attackDistance)
                {
                    nav.isStopped = false;
                    isMoving = true;
                    characterLocomotion.TryStopAbility(use);
                }
                else//攻击
                {
                    characterLocomotion.TryStartAbility(use);
                }
            }
        }

    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (hasDead)
            {
                return;
            }
            playerTrans = other.transform;
            startMoving = true;
            isMoving = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            if (hasDead)
            {
                return;
            }
            startMoving = false;
            isMoving = false;
            playerTrans = null;
        }
    }


    public void CreateBlood(float a,Vector3 b,Vector3 c,GameObject d)
    {
        GameObject bloodGo=ObjectPool.Instantiate(bloodEffect, b, Quaternion.identity);
        ObjectPool.Destroy(bloodGo,4);
    }

    public void HaveDead()
    {
        hasDead = true;
        abilities[3].Enabled = false;
        abilities[0].Enabled = false;
        abilities[1].Enabled = false;
    }

    private void GetMoveAbility()
    {
        abilities[3].Enabled = true;
    }
}
