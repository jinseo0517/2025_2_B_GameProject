using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerAI : MonoBehaviour
{
    public Transform player;        //유저위치
    public float chaseRange = 50.0f;
    public float attackRange = 2.0f;

    private NavMeshAgent agent;     //길찾기 알고리즘을 지원해주는 AI Agent
    private float distanceToPlayer; //플레이어와의 거리

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer =Vector3.Distance(transform.position, player.position);        //플레이어와의 거리를 측정한다

        if (distanceToPlayer <= chaseRange)     //추적범위에 들어오면 추적
        {
            ChasePlayer();
        }
        else
        {
            StopChasing();
        }

        if (distanceToPlayer <= attackRange)        //공격범위에 들어오면 공격
        {
            Attack();
        }
    }
    void StopChasing()
    {
        agent.isStopped = true;
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);      //플레이어의 위치를 목적지로 설정
    }
    void Attack()
    {
        agent.isStopped = true;
        transform.LookAt(player);
        Debug.Log("Attacking player!");
    }
    private void OnDrawGizmosSelected()     //오브젝트를 선택했을때 범위표시
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);      //추적범위를 노란색 구체로표시

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);     //추적범위를 빨간색 구체로표시
    }
}
