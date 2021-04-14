using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5.0f;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    NavMeshAgent nMa;
    // Start is called before the first frame update
    void Start()
    {
        nMa = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
    else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }

    }

   

    private void EngageTarget()
    {
        if (distanceToTarget >= nMa.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget >= nMa.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        nMa.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void OnDrawGizmosSelected()
    {
        //display chase radius when selected
        Gizmos.color = new Color(255f, 0f, 0f);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
  
    }
}
