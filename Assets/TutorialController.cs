using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialController : MonoBehaviour
{

    public static TutorialController instance;
    [SerializeField] TextMeshProUGUI _tp = null;

    //state
    bool _seenIntro = false;
    bool _hasRotated = false;
    bool _seenUpgradePanel = false;
    bool _usedUpgrade = false;
    bool _beenTaughtAboutHarvesting_0 = false;
    bool _beenTaughtAboutHarvesting_1 = false;
    bool _beenTaughtAboutHarvesting_2 = false;
    bool _seenAttackMode = false;
    bool _seenActivation = false;
    public bool EndedTutorial = false;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InputController.Instance.DownDepressed += HandleDown;
        InputController.Instance.UpDepressed += HandleUp;
        InputController.Instance.RotationCommanded += HandleRotate;

        GameController.Instance.EnteredAttackMode += HandleAttackMode;

        _tp.text = "Press [Down Arrow] to start.";
    }

    private void HandleDown()
    {
        if (!_seenIntro)
        {
            _tp.text = "Save the Earth from Malthusian hunger AND from space rocks! \n Use [Left/Right Arrow to rotate Earth.]";
            _seenIntro = true;
        }
        if (!_seenUpgradePanel && _hasRotated)
        {
            _tp.text = "Use [Left/Right Arrow] to select an upgrade. Hold [Down Arrow] to purchase";
            _seenUpgradePanel = true;
        }
        else if (!_usedUpgrade && _seenUpgradePanel)
        {
            _tp.text = "Upgrading costs Minerals. Upgrades cost Science if you haven't built something before. Press [Up Arrow].";
            _usedUpgrade = true;
        }


    }

    private void HandleUp()
    {
        if (!_beenTaughtAboutHarvesting_0 && _usedUpgrade)
        {
            _tp.text = "You need to harvest Minerals and Food. Hold [Up Arrow] when near blue Minerals or Red Food to Harvest.";
            _beenTaughtAboutHarvesting_0 = true;
        }

        if (!_beenTaughtAboutHarvesting_1 && _beenTaughtAboutHarvesting_0)
        {
            _tp.text = "Wait until all Food pips are full before trying to harvest Food. Press [Up Arrow].";
            _beenTaughtAboutHarvesting_1 = true;
        }


        if (!_beenTaughtAboutHarvesting_2 && _beenTaughtAboutHarvesting_1)
        {
            _tp.text = "Your citizens eat Food. You lose when your Food hits zero - Malthus, amiright? Press [Up Arrow] to finish tutorial.";
            _beenTaughtAboutHarvesting_2 = true;
        }

        if (!EndedTutorial && _beenTaughtAboutHarvesting_2)
        {
            _tp.text = " ";
            GameController.Instance.ForceImminentAttack();
            EndedTutorial = true;
        }
    }

    private void HandleRotate(int obj)
    {
        if (!_hasRotated && _seenIntro)
        {
            _tp.text = "Press [Down Arrow] to open the upgrade panel. You'll soon see that cratered sites can't be upgraded.";
            _hasRotated = true;
        }

        if (_seenAttackMode)
        {
            _tp.text = " ";
        }

    }

    private void HandleAttackMode()
    {
        if (!_seenAttackMode && _seenIntro)
        {
            _tp.text = "Oh, I forgot - the space rocks will end the game just as easily as world hunger. Use Turrets to survive!";
            _seenAttackMode = true;
        }
    }
}
