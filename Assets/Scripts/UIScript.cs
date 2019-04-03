using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {



    public Text healthTxt;
    public Text xpTxt;

    public Text StatsGuiHealth;
    public Text StatsGuiSpellPower;
    public Text StatsGuiAttackSpeed;
    public Text StatsGuiLevel;
    public Text StatsGuiAP;
    public Text StatsGuiExp;

    public Button pauseButton;
    public Button pauseCloseButton;
    public Button incHealthBtn;
    public Button incSpellPwrBtn;
    public Button incAtkSpdBtn;


    public RectTransform greenBar;
    public RectTransform blueBar;

    public GameObject StatsGuiCanvas;

    private float barwidth;
    private Vector3 greenBarPos;
    private Vector3 blueBarPos;
    private Scene previousScene;




	// Use this for initialization
	void Start () {

        //setup button event listeners
        pauseButton.onClick.AddListener(pauseButtonClicked);
        pauseCloseButton.onClick.AddListener(pauseMenuClose);
        incHealthBtn.onClick.AddListener(increaseHealth);
        incSpellPwrBtn.onClick.AddListener(increaseSpellPower);
        incAtkSpdBtn.onClick.AddListener(increaseAttackSPeed);



        //setup player health and xp bars
        barwidth = greenBar.sizeDelta.x;
        greenBarPos = greenBar.localPosition;
        blueBarPos = blueBar.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

        if (!SceneGlobals.pause)
        {
            updateHealthBar();
            updateXpBar();
        }
        else
        {
            initPauseMenu();
        }
	}

    private void updateHealthBar()
    {

        Vector3 pos = greenBar.transform.position;
        healthTxt.text = Player.currentHealth.ToString() + " / " + Player.maxHealth.ToString();
        float p = (float)Player.currentHealth / (float)Player.maxHealth;
        greenBar.transform.localScale = new Vector3(p, 1, 1);
        greenBar.transform.localPosition = new Vector3(greenBarPos.x -((1-p)*barwidth/2)  , greenBarPos.y, greenBarPos.z);
    }

    private void updateXpBar()
    {

        Vector3 pos = blueBar.transform.position;
        xpTxt.text = Player.currentXp.ToString() + " / " + Player.totalXpForLvl.ToString();
        float p = (float)Player.currentXp / (float)Player.totalXpForLvl;
        blueBar.transform.localScale = new Vector3(p, 1, 1);
        blueBar.transform.localPosition = new Vector3(blueBarPos.x - ((1 - p) * barwidth / 2), blueBarPos.y, blueBarPos.z);
    }

    public void pauseButtonClicked()
    {
        initPauseMenu();
        SceneGlobals.pause = true;
        StatsGuiCanvas.SetActive(true);
    }

    public void pauseMenuClose()
    {
        SceneGlobals.pause = false;
        StatsGuiCanvas.SetActive(false);
    }

    private void initPauseMenu()
    {
        StatsGuiHealth.text      = Player.maxHealth.ToString();
        StatsGuiLevel.text       = Player.playerLevel.ToString();
        StatsGuiExp.text         = Player.currentXp.ToString() + "\\" + Player.totalXpForLvl.ToString();
        StatsGuiSpellPower.text  = Player.intell.ToString();
        StatsGuiAP.text          = Player.AbilityPoints.ToString();
        StatsGuiAttackSpeed.text = Player.fireRate.ToString();
        
    }

    private void increaseHealth()
    {
        if (Player.AbilityPoints > 0)
        {
            Player.AbilityPoints--;
            Player.spentAP++;
            Player.maxHealth += 1;
        }
    }

    private void increaseSpellPower()
    {
        if (Player.AbilityPoints > 0)
        {
            Player.AbilityPoints--;
            Player.spentAP++;
            Player.intell += 1;
        }
    }

    private void increaseAttackSPeed()
    {
        if (Player.AbilityPoints > 0)
        {
            Player.AbilityPoints--;
            Player.spentAP++;
            Player.fireRate += 1;
        }
    }

}
