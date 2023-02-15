using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
/// <summary>
/// 手写一个AI的状态机
/// 
/// </summary>
public class StateMachine : MonoBehaviour
{

    public delegate void StartFn();
    public delegate void UpdateFn();
    public delegate void ExitFn();

    protected class State
    {
        private string _name { get; set; }
        private StartFn _fnStart { get; set; }
        private UpdateFn _fnUpdate { get; set; }
        private ExitFn _fnExit { get; set; }
        public State(string stateName, UpdateFn fnUpdate = null, StartFn fnStart = null, ExitFn fnExit = null)
        {
            _name = stateName;
            _fnStart = fnStart;
            _fnUpdate = fnUpdate;
            _fnExit = fnExit;
        }
        public void Start() { if (_fnStart != null) _fnStart(); }
        public void Update() { if (_fnUpdate != null) _fnUpdate(); }
        public void Exit() { if (_fnExit != null) _fnExit(); }
    }

    private Dictionary<string, State> _states = new Dictionary<string, State>();
    private State _currentstate;

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (_currentstate != null) { _currentstate.Update(); }
    }
    /// <summary>
    /// 添加状态，初始化
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="fnUpdate"></param>
    /// <param name="fnStart"></param>
    /// <param name="fnExit"></param>
    protected void AddState(string stateName, UpdateFn fnUpdate = null, StartFn fnStart = null, ExitFn fnExit = null)
    {
        _states.Add(stateName, new State(stateName, fnUpdate, fnStart, fnExit));
    }
    /// <summary>
    /// 设置一个状态机
    /// </summary>
    public void SetState(string stateName)
    {
        State state = _states[stateName];
        if(!IsValid(state)) return;
        SetState(state);
    }
    /// <summary>
    /// 状态切换
    /// </summary>
    /// <param name="newState"></param>
    private void SetState(State newState)
    {
        if (_currentstate != null) _currentstate.Exit();
        _currentstate = newState;
        _currentstate.Start();
    }
    /// <summary>
    /// 判断是否是新的状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    private bool IsValid(State state)
    {
        return state != null && state != _currentstate;
    }
    public bool IsEmpty()
    {
        return _states.Count == 0;
    }
}
