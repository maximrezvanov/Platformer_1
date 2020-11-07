using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffReciever : MonoBehaviour
{

    private List<Buff> buffs;
    public Action OnBuffsChanged;
    public List<Buff> Buffs
    {
        get { return buffs; }
    }


    private void Start()
    {
        GameManager.Instance.buffRecieverContainer.Add(gameObject, this);
        buffs = new List<Buff>();
    }

    public void AddBuff(Buff buff)
    {
        if (!buffs.Contains(buff))
            buffs.Add(buff);

        OnBuffsChanged?.Invoke();

    }

    public void RemoteBuff(Buff buff)
    {
        if (buffs.Contains(buff))
            buffs.Remove(buff);

        OnBuffsChanged?.Invoke();

    }

}


