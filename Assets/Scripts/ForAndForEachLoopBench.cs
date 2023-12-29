using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

//C# ve Unity framework Debug Method Conflicts
using Debug = UnityEngine.Debug;

public class ForAndForEachLoopBench : MonoBehaviour {
    [SerializeField] [Range(0, 100000000)] int _numberOfItems = 100;

    UnknownType _unknownTypeForLoop;
    UnknownType _unknownTypeForEach;

    //Unknown Type Lists
    List<UnknownType> _forLoopUnknowns = new List<UnknownType>();
    List<UnknownType> _forEachUnknowns = new List<UnknownType>();

    Stopwatch _stopWatchForLoop = new Stopwatch();
    Stopwatch _stopWatchForEach = new Stopwatch();

    int _thWorkers, _thCompletionWorkers;
    async void Start() {
        string message = 
            @$"Main Thread Id {Thread.CurrentThread.ManagedThreadId}.";
        Debug.LogWarning(message);
        await RunAllAsync();
    }

    void Update() {

    }

    public void CreateWith_ForLoop() {
        _forLoopUnknowns.Clear();
        Debug.Log($"For Loop Is Starting... Number Of Element {PartDigits()}, Thread {Thread.CurrentThread.ManagedThreadId}.");
        _stopWatchForLoop.Start();
        for(int i = 0; i < _numberOfItems; i++) {
            _forLoopUnknowns.Add(new UnknownType());
        }
        _stopWatchForLoop.Stop();
        Debug.Log($"For Loop {PartDigits()} List Creation Has Ended In {_stopWatchForLoop.ElapsedMilliseconds} ms.");
    }

    public void CreateWith_ForEach() {
        _forEachUnknowns.Clear();
        Debug.Log($"For Each Is Starting... Number Of Element {PartDigits()}, Thread {Thread.CurrentThread.ManagedThreadId}.");
        _stopWatchForEach.Start();
        foreach (int i in Enumerable.Range(0, _numberOfItems)) {
            _forEachUnknowns.Add(new UnknownType());
        }
        _stopWatchForEach.Stop();
        Debug.Log($"For Each {PartDigits()} List Creation Has Ended In {_stopWatchForEach.ElapsedMilliseconds} ms.");
        _stopWatchForEach.Reset();
    }

    public void GetElementFromForLoopList() {
        Debug.Log($"For Loop Accessing To Elements Is Starting... Number Of Element {PartDigits()}, Thread {Thread.CurrentThread.ManagedThreadId}.");
        _stopWatchForLoop.Start();
        for (int i = 0; i < _numberOfItems; i++) {
            _unknownTypeForLoop = _forLoopUnknowns[i];
        }
        _stopWatchForLoop.Stop();
        Debug.Log($"For Loop {PartDigits()}, Accessing To Elements Has Ended In {_stopWatchForLoop.ElapsedMilliseconds} ms.");
        _forLoopUnknowns.Clear();
    }

    public void GetElementFromForEachList() {
        Debug.Log($"For Each Accessing To Elements Is Starting... Number Of Element {PartDigits()}, Thread {Thread.CurrentThread.ManagedThreadId}.");
        _stopWatchForEach.Start();
        foreach (UnknownType ut in _forEachUnknowns) {
            _unknownTypeForEach = ut;
        }
        _stopWatchForEach.Stop();
        Debug.Log($"For Each {PartDigits()}, Accessing To Elements Has Ended In {_stopWatchForEach.ElapsedMilliseconds} ms.");
        _forEachUnknowns.Clear();
    }

    public void RunAll() {
        CreateWith_ForLoop();
        CreateWith_ForEach();
        GetElementFromForLoopList();
        GetElementFromForEachList();
    }

    public async Task RunAllAsync() {
        await Task.Run(RunAll);
        Debug.Log("Benchmark Has Ended.");
        GC.Collect();
    }

    public async Task RunAsync() {
        Task forLoopCreationTask = Task.Run(CreateWith_ForLoop);
        Task forEachCreationTask = Task.Run(CreateWith_ForEach);

        await Task.WhenAll(forLoopCreationTask, forEachCreationTask);

        Task forLoop_AccessingToElements = Task.Run(GetElementFromForLoopList);
        Task forEach_AccessingToElements = Task.Run(GetElementFromForEachList);

        await Task.WhenAll(forLoop_AccessingToElements, forEach_AccessingToElements);

        Debug.LogWarning("Bench Has Ended!");
        GC.Collect();
    }

    public string PartDigits() {
        string nois = _numberOfItems.ToString();
        char[] charArray = nois.ToCharArray();
        charArray = charArray.Reverse().ToArray();
        nois = new string(charArray);

        string result = "";
        for (int i = 0; i < nois.Length; i++) {
            result += nois[i];
            if ((i + 1) % 3 == 0 && i != nois.Length - 1) {
                result += " ";
            }
        }
        charArray = result.ToCharArray();
        charArray = charArray.Reverse().ToArray();
        return new string(charArray);
    }
}


