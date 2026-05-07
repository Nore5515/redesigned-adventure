using UnityEngine;

namespace NPCs
{
    [CreateAssetMenu(fileName = "NPCTalkingSprites", menuName = "NPCTalkingSprites")]
    public class NPCTalkingSpriteSO: ScriptableObject
    {
        public Sprite[] sprites;
    }
}