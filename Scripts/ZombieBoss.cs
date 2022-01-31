using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateCharacterController.Character.Abilities.Items;

public class ZombieBoss : Zombie
{
    private EquipNext equipNext;

    private EquipUnequip equipUnequip;

    private Reload reload;

    private bool changeWeapon;

    private bool useShootableWeapon;

    private bool useMeleeweapon;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        equipNext = characterLocomotion.GetAbility<EquipNext>();
        equipUnequip = characterLocomotion.GetAbility<EquipUnequip>();
        reload = characterLocomotion.GetAbility<Reload>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    characterLocomotion.TryStartAbility(equipNext);
        //    //equipUnequip.StartEquipUnequip(1);
        //}

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
                    if (Vector3.Distance(playerTrans.position,nav.transform.position)>attackDistance*6)
                    {
                        useShootableWeapon = true;
                        changeWeapon = true;
                        startMoving = false;
                        useMeleeweapon = false;
                    }

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
                    if (Vector3.Distance(playerTrans.position, nav.transform.position) < attackDistance / 2)
                    {
                        if (!useMeleeweapon)
                        {
                            useMeleeweapon = true;
                            useShootableWeapon = false;
                            changeWeapon = true;
                            startMoving = false;
                        }
                    }
      
                    characterLocomotion.TryStopAbility(use);
                    characterLocomotion.TryStartAbility(use);
                }
            }
        }
        else
        {
            if (changeWeapon)
            {
                characterLocomotion.TryStopAbility(use);
                characterLocomotion.TryStopAbility(reload);
                changeWeapon = false;
                characterLocomotion.TryStartAbility(equipNext);
                Invoke("RestartMoving", 0.5f);
            }
        }

    }

    private void RestartMoving()
    {
        startMoving = true;
        if (useMeleeweapon)
        {
            attackDistance = 1.3f;
        }
        if (useShootableWeapon)
        {
            attackDistance = 8;
        }
    }

    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    private new void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
