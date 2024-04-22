using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public QuestUI questUI;

    // Start is called before the first frame update
    void Start()
    {
        questUI.OpenBox();

        StartCoroutine(QuestDemo());
    }

    private IEnumerator QuestDemo()
    {
        questUI.OpenBox();
        
        yield return new WaitForSeconds(1.0f);

        questUI.WriteText("OBJECTIVE: kill all of the cylinders (evil)");

        yield return new WaitForSeconds(5.0f);

        questUI.CloseBox();

        // TODO: Space to advance dialog
    }

    public void DeathScreen()
    {
        SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
