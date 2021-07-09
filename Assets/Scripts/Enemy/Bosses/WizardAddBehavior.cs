using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAddBehavior : MonoBehaviour
{
    [SerializeField] private VoidEvent addDeadEvent;

    private void OnDestroy()
    {
        addDeadEvent.Raise();
    }
}
