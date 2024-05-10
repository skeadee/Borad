using UnityEngine;

public class Ch_Unit : MonoBehaviour
{
    Ch_GameManager GameManager;
   
    string Attack_Tag;
    public bool Rook, Bishop, Queen , Knight , King;
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

    }


    void Location_Prefabs(Vector3[] Dir) // ��ġ Ȯ�� �� ������ ��ȯ
    {

        Vector3 pos = transform.position;
        pos.z = -15;
        RaycastHit hit;

        for (int i = 0; i < Dir.Length; i++)
        {

            Vector3 Loc = (transform.position + Dir[i]);
            Vector3 end = (Loc - pos).normalized;

            if (Physics.Raycast(pos, end, out hit, 20f))
            {

               
                if (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Respawn") // �ƹ��͵� ���� �� �ڸ����
                {
                    Instantiate(GameManager.Click, Loc, Quaternion.identity);
                }

                else if (hit.collider.tag == Attack_Tag) // ���� �浹 �Ѵٸ�
                {
                    GameObject Enemy = Instantiate(GameManager.Click, Loc, Quaternion.identity);
                    Enemy.GetComponent<Ch_Click_Move>().Enemy_Set(hit.collider.gameObject);

                    if(!Knight && !King) break; // ���� �浹 �� �迭 �޺κ� �������
                }

                else if(!King && !Knight) // �Ʊ��� �浹�ϰ� , king �� knight�� �ƴ϶��(�� 2���� �迭�� ���Ⱚ�� �ϳ��� �迭�� �޾Ƽ� ���ܷ� �ľ���)
                {
                    break;
                }

            }

        }

    }


    void OnMouseDown()
    {
        if (GameManager.Turn != Turn_Color || GameManager.GameEnd) return;
        if (GameManager.Target == gameObject) return;
        
     
        GameManager.Destory_Move(); 
        GameManager.Target = gameObject;

        if(Rook)
        {
            Location_Prefabs(GameManager.Right);
            Location_Prefabs(GameManager.Left);
            Location_Prefabs(GameManager.Up);
            Location_Prefabs(GameManager.Down);
        }

        else if(Bishop)
        {
            Location_Prefabs(GameManager.UR);
            Location_Prefabs(GameManager.DR);
            Location_Prefabs(GameManager.UL);
            Location_Prefabs(GameManager.DL);
        }

        else if(Queen)
        {
            Location_Prefabs(GameManager.Right);
            Location_Prefabs(GameManager.Left);
            Location_Prefabs(GameManager.Up);
            Location_Prefabs(GameManager.Down);
            Location_Prefabs(GameManager.UR);
            Location_Prefabs(GameManager.DR);
            Location_Prefabs(GameManager.UL);
            Location_Prefabs(GameManager.DL);
        }

        else if(Knight)
        {
            Location_Prefabs(GameManager.Knight_Move);
        }

        else if(King)
        {
            Location_Prefabs(GameManager.King_Move);
        }
        


    }

   




}
