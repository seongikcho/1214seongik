using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreBuyText : MonoBehaviour
{
    public Text BuyAtkText;
    public Text BuyHpText;
    public Text BuyCircleSpeedText;
    public Text BuyCircleSizeText;
    public GameManger gameManger;


    private void Awake()
    {
        gameManger = GetComponent<GameManger>();
        BuyAtkText.text = gameManger.AttackPoint_Price.ToString() + "p";
        BuyHpText.text =  gameManger.HpPoint_Price.ToString() + "p";
        BuyCircleSpeedText.text = gameManger.CircleSpeed_Price.ToString() + "p";
        BuyCircleSizeText.text = gameManger.CircleSize_Price.ToString() + "p";

    }


    void Update()
    {
        
    }

    public void Buy_ATTACKPOINT_Text()
    {
        BuyAtkText.text = gameManger.AttackPoint_Price.ToString() + "p";
    }
    public void Buy_HPPOINT_Text()
    {
        BuyHpText.text = gameManger.HpPoint_Price.ToString() + "p";
    }
    public void Buy_CircleSpeed_Text()
    {
        BuyCircleSpeedText.text = gameManger.CircleSpeed_Price.ToString() + "p";
    }
    public void Buy_CircleSize_Text()
    {
        BuyCircleSizeText.text = gameManger.CircleSize_Price.ToString() + "p";
    }
}

