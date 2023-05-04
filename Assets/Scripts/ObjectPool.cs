using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using System;

public class ObjectPool : NinjaMonoBehaviour {
    [SerializeField] private uint initPoolSize;
    [SerializeField] private PooledObject objectToPool;
    public System.Action OnPoolClear;
    private Stack<PooledObject> stack;

    private void Awake() {
        SetupPool();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.W)) {
            GetPooledObject();        
        }
    }
    private void SetupPool() {
        var logId = "SetupPool";
        stack = new Stack<PooledObject>();
        PooledObject pooledObject = null;

        for (int i = 0; i < initPoolSize; i++) {
            pooledObject = Instantiate(objectToPool);
            pooledObject.Pool = this;
            pooledObject.gameObject.SetActive(false);
            stack.Push(pooledObject);       
        }
    }
    public void Clear() {
        OnPoolClear?.Invoke();
    }

    public PooledObject GetPooledObject() {
        var logId = "GetPooledObject";
        if(stack.Count==0) {
            PooledObject newObject = Instantiate(objectToPool);
            newObject.Pool = this;
            logd(logId, "No objects of type "+newObject.GetType()+" in stack. Instantiated new object="+newObject.logf()+" for pool="+name+" => returning new object");
            return newObject;
        }
        PooledObject nextObject = stack.Pop();
        nextObject.gameObject.SetActive(true);
        logd(logId, "Popped object="+nextObject.logf()+" from stack => returning object");
        return nextObject;
    }

    public void ReturnToPool(PooledObject pooledObject) {
        var logId = "ReturnToPool";
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
