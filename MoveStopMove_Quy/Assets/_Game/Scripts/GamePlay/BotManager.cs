using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : Singleton<BotManager>
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] GameObject Botprefab;
    [SerializeField] List<GameObject> Botlist = new List<GameObject>();
    private bool isplay=false;

    private void Start()
    {
    }

    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay) )
        {
            if (variableJoystick.Horizontal != 0 || variableJoystick.Vertical != 0)
            {
                isplay = true;
            }
            if (isplay && Botlist.Count < 10)
            {
                SpawnAI();
            }
        }
    }
    public void SpawnAI()
    {
        GameObject obj = Instantiate(Botprefab);
        float x = Random.Range(-33, 25);
        float z = Random.Range(-33, 25);
        obj.transform.position = new Vector3(x, 0.5f, z);
        Botlist.Add(obj);
        IndicatorManager.Instance.CreateNewIndicator(obj.GetComponent<Character>());
    }

    public List<GameObject> getList()
    {
        return Botlist;
    }

    public GameObject getBot()
    {
        return Botlist[Botlist.Count - 1];
    }

    public void setIsPlay(bool a)
    {
        isplay = a;
    }
}
