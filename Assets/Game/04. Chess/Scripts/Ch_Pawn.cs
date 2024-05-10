using UnityEngine;

public class Ch_Pawn : MonoBehaviour
{
    Ch_GameManager GameManager;
    public bool First = true;

    string Attack_Tag;
    bool white = false;
    int Turn_Color;
   
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<Ch_GameManager>();

        if (gameObject.tag == "White")
        {
            Attack_Tag = "Black";
            Turn_Color = 2;
        }

        else
        {
            Attack_Tag = "White";
            Turn_Color = 1;
        }

        if (gameObject.tag == "White") white = true;
    }


    void Location_Prefabs(Vector3[] Dir) // ��ġ �̵�
    {

        Vector3 pos = transform.position;
        pos.z = -15;
        RaycastHit hit;

        for (int i = 0; i < Dir.Length; i++)
        {
          
            Vector3 Loc = transform.position + Dir[i];
            Vector3 end = (Loc - pos).normalized;
           

            if (Physics.Raycast(pos, end, out hit, 20f))
            {
                // �ƹ��͵� ���� ������̶�� , ������ Ŭ���� ������ �̵����� ������ ������Ʈ�� �� ��� ���ٸ�
                if ((hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Respawn"))
                {
                    Instantiate(GameManager.Click, Loc, Quaternion.identity);
                }

                

            }

        }

    }

    void Attack(Vector3[] Dir)
    {

        Vector3 pos = transform.position;
        pos.z = -15;
        RaycastHit hit;

        for (int i = 0; i < Dir.Length; i++)
        {

            Vector3 Loc = transform.position + Dir[i];
            Vector3 end = (Loc - pos).normalized;


            if (Physics.Raycast(pos, end, out hit, 20f))
            {
                if (hit.collider.gameObject.tag == Attack_Tag)
                {
                    GameObject Enemy =  Instantiate(GameManager.Click, Loc, Quaternion.identity);
                    Enemy.GetComponent<Ch_Click_Move>().Enemy_Set(hit.collider.gameObject);

                }

            }

        }

    }



    void OnMouseDown()
    {
        if (GameManager.GameEnd) return;
        if (GameManager.Turn != Turn_Color || GameManager.Target == gameObject) return;

        Destory_Move();

        GameManager.Target = gameObject;

        
        if(white)
        {
            if (First) Location_Prefabs(GameManager.First_Pawn_Move2);
            else Location_Prefabs(GameManager.Pawn_Move2);

            Attack(GameManager.Pawn_Attack2);
        }

        else
        {
            if (First) Location_Prefabs(GameManager.First_Pawn_Move); // �������� ���
            else Location_Prefabs(GameManager.Pawn_Move);

            Attack(GameManager.Pawn_Attack); // ������ ���
        }
        

       

    }

    void Destory_Move()
    {
        GameObject[] tag = GameObject.FindGameObjectsWithTag("Respawn");

        for (int i = 0; i < tag.Length; i++) Destroy(tag[i]);
    }

}
