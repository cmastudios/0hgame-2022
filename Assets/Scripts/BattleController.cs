using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class BattleController : MonoBehaviour
{
    public Text status;
    private bool offensive = true;
    public static bool battleWon = false;

    public Slider americaHealth, opponentHealth;

    enum Move
    {
        NONE,
        FIGHT,
        WAIT
    };
    enum BattleState
    {
        WAIT_FOR_INPUT,
        PLAYER_ATTACK,
        PLAYER_SKIP,
        PLAYER_CROWD_REACT,
        OPPONENT_MOVE,
        OPPONENT_ATTACK,
        OPPONENT_SKIP,
        OPPONENT_CROWD_REACT,
        VICTORY,
        FAILURE,
        HALT
    };

    private BattleState battleState;
    private float stateStartTime;
    private Move requestedPlayerMove;

    private BattleState GetDesiredState()
    {
        BattleState desiredState = battleState;
        switch (battleState)
        {
            case BattleState.WAIT_FOR_INPUT:
                switch (requestedPlayerMove)
                {
                    case Move.FIGHT:
                        desiredState = BattleState.PLAYER_ATTACK;
                        break;
                    case Move.WAIT:
                        desiredState = BattleState.OPPONENT_MOVE;
                        break;
                    case Move.NONE:
                    default:
                        // none
                        break;
                }
                break;
            case BattleState.PLAYER_ATTACK:
                if (Time.time - stateStartTime > 3.0f)
                {
                    desiredState = BattleState.OPPONENT_MOVE;
                    if (americaHealth.value >= 1.0f)
                    {
                        if (Random.Range(0.0f, 1.0f) < 0.8f) desiredState = BattleState.PLAYER_CROWD_REACT;
                    }
                }
                break;
            case BattleState.PLAYER_CROWD_REACT:
                if (Time.time - stateStartTime > 3.0f) desiredState = BattleState.OPPONENT_MOVE;
                break;
            case BattleState.OPPONENT_MOVE:
                desiredState = BattleState.OPPONENT_SKIP;
                if (opponentHealth.value <= 0.0f)
                {
                    desiredState = BattleState.VICTORY;
                }
                else if (opponentHealth.value < 1.0f)
                {
                    if (Random.Range(0.0f, 1.0f) < 0.8f) desiredState = BattleState.OPPONENT_ATTACK;
                }
                else
                {
                    if (Random.Range(0.0f, 1.0f) < 0.5f) desiredState = BattleState.OPPONENT_ATTACK;
                }
                break;
            case BattleState.OPPONENT_ATTACK:
                if (Time.time - stateStartTime > 3.0f)
                {
                    if (americaHealth.value <= 0.0f)
                    {
                        desiredState = BattleState.FAILURE;
                    }
                    else if (Random.Range(0.0f, 1.0f) < 0.8f)
                    {
                        desiredState = BattleState.OPPONENT_CROWD_REACT;
                    }
                    else
                    {
                        desiredState = BattleState.WAIT_FOR_INPUT;
                    }
                }
                break;
            case BattleState.OPPONENT_SKIP:
                if (Time.time - stateStartTime > 3.0f) desiredState = BattleState.WAIT_FOR_INPUT;
                break;
            case BattleState.OPPONENT_CROWD_REACT:
                if (Time.time - stateStartTime > 3.0f) desiredState = BattleState.WAIT_FOR_INPUT;
                break;
            case BattleState.VICTORY:
                if (Time.time - stateStartTime > 3.0f) desiredState = BattleState.HALT;
                break;
            case BattleState.FAILURE:
                if (Time.time - stateStartTime > 3.0f) desiredState = BattleState.HALT;
                break;
            default:
                break;
        }
        return desiredState;
    }

    private void RunEntryAction()
    {
        switch (battleState)
        {
            case BattleState.WAIT_FOR_INPUT:
                Button[] buttons = FindObjectsOfType<Button>();
                foreach (Button button in buttons)
                {
                    button.enabled = true;
                }
                requestedPlayerMove = Move.NONE;
                status.text = "What will America do?";
                break;
            case BattleState.PLAYER_ATTACK:
                float attackDamage = Random.Range(0.3f, 0.7f);
                opponentHealth.value = Mathf.Max(0.0f, opponentHealth.value - attackDamage);
                status.text = "America used attack!";
                break;
            case BattleState.PLAYER_CROWD_REACT:
                attackDamage = Random.Range(0.6f, 0.9f);
                americaHealth.value = Mathf.Max(0.0f, americaHealth.value - attackDamage);
                status.text = "..the public disapproved!";
                break;
            case BattleState.OPPONENT_ATTACK:
                attackDamage = Random.Range(0.2f, 0.5f);
                americaHealth.value = Mathf.Max(0.0f, americaHealth.value - attackDamage);
                status.text = "\"Opponent attacked\"!";
                break;
            case BattleState.OPPONENT_SKIP:
                status.text = "Opponent is watching closely!";
                break;
            case BattleState.OPPONENT_CROWD_REACT:
                float attackRegen = Random.Range(0.01f, 0.2f);
                americaHealth.value = Mathf.Min(1.0f, americaHealth.value + attackRegen);
                status.text = "..the public is angry!";
                break;
            case BattleState.VICTORY:
                status.text = "America gained land for winning!";
                break;
            case BattleState.FAILURE:
                status.text = "America fainted!";
                break;
            default:
                break;
        }
    }

    private void RunExitAction()
    {
        switch (battleState)
        {
            case BattleState.WAIT_FOR_INPUT:
                Button[] buttons = FindObjectsOfType<Button>();
                foreach (Button button in buttons)
                {
                    button.enabled = false;
                }
                break;
            case BattleState.VICTORY:
                GameObject.Find("LevelController").GetComponent<LevelController>().EndCubaBattle(true);
                break;
            case BattleState.FAILURE:
                GameObject.Find("LevelController").GetComponent<LevelController>().EndCubaBattle(false);
                break;
            default:
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        battleState = BattleState.WAIT_FOR_INPUT;
        americaHealth.value = 1.0f;
        opponentHealth.value = 1.0f;
        RunEntryAction();
    }

    // Update is called once per frame
    void Update()
    {
        BattleState desiredState = GetDesiredState();
        if (desiredState != battleState)
        {
            RunExitAction();
            battleState = desiredState;
            stateStartTime = Time.time;
            RunEntryAction();
        }
    }

    public void Fight()
    {
        requestedPlayerMove = Move.FIGHT;
    }

    public void Wait()
    {
        requestedPlayerMove = Move.WAIT;
    }


}
