using System;
using UnityEditor.Build;

namespace NPCs
{
    [Serializable]
    public class DialogueLine
    {
        public string text;
        public NPCTalkingSpriteSO talkingSprites;
        public float duration;
        public NPCTalkingSpriteSO endSprites;
    }
}