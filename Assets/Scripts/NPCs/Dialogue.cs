using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue", fileName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<string> lines;
}
