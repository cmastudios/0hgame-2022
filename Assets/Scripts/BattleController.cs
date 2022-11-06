using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BattleController : MonoBehaviour
{
    public Text status;
    private bool offensive = true;
    public static bool battleWon = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fight()
    {
        if (offensive)
        {
            StartCoroutine(AggressiveCoroutine());
        }
        else
        {
            StartCoroutine(DefensiveCoroutine());
        }
    }

    public void Wait()
    {
        StartCoroutine(TauntCoroutine());
    }

    private IEnumerator TauntCoroutine()
    {
        status.text = "\"Opponent attacked our ship\"!";

        yield return new WaitForSeconds(3);

        status.text = "..the public is angry!";

        yield return new WaitForSeconds(3);

        status.text = "What will America do?";

        offensive = false;
    }

    private IEnumerator AggressiveCoroutine()
    {
        status.text = "America used attack!";

        yield return new WaitForSeconds(3);

        status.text = "..the public disapproved!";

        yield return new WaitForSeconds(3);

        status.text = "What will America do?";
    }

    private IEnumerator DefensiveCoroutine()
    {
        status.text = "America used attack!";

        yield return new WaitForSeconds(3);

        status.text = "It was super effective!";

        yield return new WaitForSeconds(3);

        status.text = "Opponent fainted!";

        yield return new WaitForSeconds(3);

        status.text = "America gained land for winning!";

        yield return new WaitForSeconds(3);

        battleWon = true;

        GameObject.Find("LevelController").GetComponent<LevelController>().EndCubaBattle();
    }

}
