using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatText : MonoBehaviour
{

    public Text PlayerATKText;
    public Text PlayerHPText;
    public Text AbilityPointText;
    public Text PlayerMoney;
    public Text PlayerTotalDmgText;
    public Text CircleSpeedText;
    public Text CircleSizeText;
    public PlayerMoving playerMoving;
    public CircleController circleController;


    float PlayerAtkDmg2;
    private void Awake()
    {
        AbilityPointText.text = playerMoving.AbilityPoint.ToString();
        PlayerATKText.text = playerMoving.PlayerAtkDmg.ToString();
        PlayerHPText.text = playerMoving.PlayerHp.ToString();
        PlayerAtkDmg2 = playerMoving.PlayerAtkDmg * 2;
        PlayerTotalDmgText.text = playerMoving.PlayerAtkDmg.ToString() + " ~ " + PlayerAtkDmg2.ToString();
        PlayerMoney.text = "x "+ playerMoving.Money.ToString();
        CircleSpeedText.text = "5초당 "+ circleController.AngleSpeed.ToString() +"회 회전";
        CircleSizeText.text = circleController.CircleSize_var.ToString() + " ft";
    }
    public void StatTextUpLoad()
    {
        AbilityPoint_Text();
        PlayerATK_Text();
        PlayerHP_Text();
        PlayerTotalDmg_Text();
    }

    public void AbilityPoint_Text()
    {
        AbilityPointText.text = playerMoving.AbilityPoint.ToString();
    }
    public void PlayerATK_Text()
    {
        PlayerATKText.text = playerMoving.PlayerAtkDmg.ToString();
    }
    public void PlayerHP_Text()
    {
        PlayerHPText.text = playerMoving.PlayerHp.ToString();
    }
    public void PlayerTotalDmg_Text()
    {
        PlayerAtkDmg2 = playerMoving.PlayerAtkDmg * 2;
        PlayerTotalDmgText.text = playerMoving.PlayerAtkDmg.ToString() + " ~ " + PlayerAtkDmg2.ToString();
    }
    public void PlayerMoney_Text()
    {
        PlayerMoney.text = "x " + playerMoving.Money.ToString();
    }
    public void CircleSpeed_Text()
    {
        CircleSpeedText.text = "5초당 " + circleController.AngleSpeed.ToString() + "회 회전";
    }
    public void CircleSize_Text()
    {
        CircleSizeText.text = circleController.CircleSize_var.ToString()+" ft";
    }

    public void StatAtkUpButton()
    {
        if (playerMoving.AbilityPoint >= 1)
        {
            playerMoving.AbilityPoint -= 1;
            playerMoving.PlayerAtkDmg += 1;
            PlayerATK_Text();
            AbilityPoint_Text();
            PlayerTotalDmg_Text();
        }
    }
    public void StatHpUpButton()
    {
        if (playerMoving.AbilityPoint >= 1)
        {
            playerMoving.AbilityPoint -= 1;
            playerMoving.PlayerHp += 10;
            PlayerHP_Text();
            AbilityPoint_Text();
            PlayerTotalDmg_Text();
        }
    }

    
}
