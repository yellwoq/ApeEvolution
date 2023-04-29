using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ApeEvolution
{
    public class PlayVoiceCommand : AbstractCommand
    {
        private string clipName;
        private Vector3 playPos; 
        public PlayVoiceCommand(string clipName, Vector3 playPos)
        {
            this.clipName = clipName;
            this.playPos = playPos;
        }

        protected override void OnExecute()
        {
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Voices/"+clipName), playPos, 1);
        }
    }
}
