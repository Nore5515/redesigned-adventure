using System.Collections.Generic;
using NPCs;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue", fileName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<DialogueLine> lines;
}
