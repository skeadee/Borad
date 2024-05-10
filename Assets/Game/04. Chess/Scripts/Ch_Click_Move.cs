using UnityEngine;

public class Ch_Click_Move : MonoBehaviour
{
    Ch_GameManager GameManager;

    Vector3 org_Loc;
    public Vector3 Start_Move;
    public GameObject enemy;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<Ch_GameManager>();

        org_Loc = transform.position;
        gameObject.transform.position = org_Loc + Start_Move;
    }

    public void Enemy_Set(GameObject target)
    {
        enemy = target;
    }


    void OnMouseDown()
    {
        GameManager.Target.transform.position = org_Loc;
        if (GameManager.Target.name == "pawn") GameManager.Target.GetComponent<Ch_Pawn>().First = false;

        GameManager.Turn_Change();
        GameManager.Target_Reset();

        if (enemy != null) Destory_Target();
      

        Destory_Move();
    }

    void Destory_Target() // 타겟 오브젝트를 제거할 때
    {
        GameManager.GameEnd_Check(enemy);
        Destroy(enemy);
       
    }


    void Destory_Move()
    {
        GameObject[] tag = GameObject.FindGameObjectsWithTag("Respawn");

        for (int i = 0; i < tag.Length; i++) Destroy(tag[i]);
    }


}
