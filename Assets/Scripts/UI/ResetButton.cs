using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    public class ResetButton : MonoBehaviour
    {

        public void ResetOrb() => CheckpointManager.instance.TeleportToCheckpoint(0);

    }
}
