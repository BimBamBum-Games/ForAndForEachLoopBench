using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// Unity 5.6' dan sonra ForEach dongusu garbage uretmiyor. Ayrica For Loop ile karsilastirildiginda cok cok daha hizlidir.
/// Burada diger ilginc olan sey ise Interface yapilarla kullanildiginda garbage yapiyor ve IEnumerable donduruyor.
/// </summary>
public class LoopGarbageBench : MonoBehaviourBase {
    [Header("The number of elements on the list will be tested on ForEach")]
    [SerializeField] [Range(0, 1000000)] int _numberOfElement = 100;
    List<UnknownType> _unknowns;
    UnknownType _unknownForEach;

    [Header("Garbage will be shown here to test it out")]
    [SerializeField] long _total;
    bool _warningMessageTick = false;

    [Header("To See the changes on mem")]
    [SerializeField] bool _garbageTick = false;

    void Start () {
        _unknowns = new List<UnknownType>();
        for(int i = 0; i < _numberOfElement; i++) {
            _unknowns.Add(new UnknownType());
        }
        Dlog($"List Count > " + _unknowns.Count);
    }

    void Update () {
        if (!_warningMessageTick) {
            Dlog($"Update Runs On ThreadId : {Thread.CurrentThread.ManagedThreadId}");
            _warningMessageTick = true;
        }
        BeginForEach();
    }

    private void BeginForEach() {
        _total = 0;
        long garbageBefore = GC.GetTotalMemory(false);
        foreach(UnknownType unknown in _unknowns) {
            _unknownForEach = unknown;
        }
        long garbageAfter = GC.GetTotalMemory(false);
        _total = garbageAfter - garbageBefore; 
        if(_total > 0) {
            _garbageTick = !_garbageTick;
        }
    }
}