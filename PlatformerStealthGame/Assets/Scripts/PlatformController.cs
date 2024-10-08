using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
public Transform posA,posB;
public int Speed;
Vector2 targetPos;

void Start()
 {
 targetPos = posB.position;
 }

void Update()
 {
 if(Vector2.Distance(transform.position,posA.position) < .1f) targetPos = posB.position;
 if(Vector2.Distance(transform.position,posB.position) < .1f) targetPos = posA.position;

 transform.position=Vector2.MoveTowards(transform.position,targetPos, Speed * Time.deltaTime);
 }
private void OnDrawGizmos()
  {
  Gizmos.color = Color.yellow;
  Gizmos.DrawLine(posA.position,posB.position);
  }
}
