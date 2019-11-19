using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu (menuName = "Assets/Combat/Input")]
public class InputCheck : MonoBehaviour
{
    public string[] inputAxis;

    public bool isFollower;
    public ActionObject followingAttack;


}
