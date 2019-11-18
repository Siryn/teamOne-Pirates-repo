using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{

    public enum EMode
    {
        AWARE,
        UNAWARE
    }

    private EMode m_currentMode;
    public EMode CurrentMode
    {
        get
        {
            return m_currentMode;
        }
        set
        {
            if (m_currentMode == value)
                return;

            m_currentMode = value;

            if (OnModeChanged != null)
                OnModeChanged(value);
        }
    }

    public event System.Action<EMode> OnModeChanged;

    private void Awake()
    {
        CurrentMode = EMode.UNAWARE;
    }

}
