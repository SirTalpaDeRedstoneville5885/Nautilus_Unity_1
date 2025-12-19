using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject pos1, pos2, pos3, nextPos;
    [SerializeField] float speed;
    [SerializeField] GameObject enemyBody;
    [SerializeField] String Tipologia;
    //[SerializeField] Animator Anim;
    [SerializeField] GameObject EnemyAnimBody;
    bool lap = true;
    void FixedUpdate()
    {
        if (Vector2.Distance(enemyBody.transform.position, nextPos.transform.position) >= 0)
        // muove il nemico verso una posizione
        {
            enemyBody.transform.position = Vector2.MoveTowards(enemyBody.transform.position, nextPos.transform.position, speed * Time.deltaTime);
        }
        // setta la prossima posizione in base a quella attuale, se si è in 3 manda in 2 e se si è in 1 manda in 2, e setta un bool per aver o meno completato un giro
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
        // controlla se il giro è completo e 
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
        // gestisce la direzione dello slime
        if (Tipologia == "NonSlime") EnemyAnimBody.transform.localRotation = lap ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        else EnemyAnimBody.transform.localRotation = lap ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        if (EnemyAnimBody.GetComponent<Animator>() != null) EnemyAnimBody.GetComponent<Animator>().SetBool("Moving", true);
    }
}
