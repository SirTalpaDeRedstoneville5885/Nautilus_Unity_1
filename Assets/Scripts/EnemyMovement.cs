using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject pos1, pos2, pos3, nextPos;
    [SerializeField] float speed;
    [SerializeField] GameObject enemyBody;
    //[SerializeField] Animator Anim;
    [SerializeField] GameObject EnemyAnimBody;
    bool lap = true;
    void FixedUpdate()
    {
        if (Vector2.Distance(enemyBody.transform.position, nextPos.transform.position) >= 0)
        {
            enemyBody.transform.position = Vector2.MoveTowards(enemyBody.transform.position, nextPos.transform.position, speed * Time.deltaTime);
        }
        if (Vector2.Distance(enemyBody.transform.position, pos1.transform.position) == 0)
        {
            nextPos = pos2;
            lap = true;
        }
        if (Vector2.Distance(enemyBody.transform.position, pos3.transform.position) == 0)
        {
            nextPos = pos2;
            lap = false;
        }
        if (lap == false)
        {
            if (Vector2.Distance(enemyBody.transform.position, pos2.transform.position) == 0)
            {
                nextPos = pos1;
            }
        }
        if (lap)
        {
            if (Vector2.Distance(enemyBody.transform.position, pos2.transform.position) == 0)
            {
                nextPos = pos3;
            }
        }
        //Anim.SetBool("Moving", lap);
        EnemyAnimBody.transform.localRotation = (lap) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }
}
