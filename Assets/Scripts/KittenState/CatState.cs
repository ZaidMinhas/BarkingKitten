using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class CatState
{
    
    protected string name;
    public abstract void Start(BarkingKitten cat);
    public abstract void Update(BarkingKitten cat);
    
    public abstract void End(BarkingKitten cat);
}