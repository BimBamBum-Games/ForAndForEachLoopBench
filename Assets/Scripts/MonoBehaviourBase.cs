using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourBase : MonoBehaviour {
    //Loglamak ve diger maksatlarla kullanilacak olan sinif.
    enum DebugType {
        Standart,
        Warning,
        Assertion
    }

    [SerializeField] bool _canDebugLogs;
    [SerializeField] DebugType _debugType = DebugType.Standart;

    /// <summary>
    /// Debug.Log yerine OnInspector uzerinde testi ac veya kapat seklinde kolaylik saglar. 
    /// </summary>
    /// <param name="message"></param>
    
    protected void Dlog(string message) {
        if (_canDebugLogs) {
            switch (_debugType) {
                case DebugType.Standart:
                    Debug.Log(message);
                    break;
            case DebugType.Warning:
                    Debug.LogWarning(message);
                    break;
            case DebugType.Assertion:
                    Debug.LogAssertion(message);
                    break;
            }
        }  
    }
}