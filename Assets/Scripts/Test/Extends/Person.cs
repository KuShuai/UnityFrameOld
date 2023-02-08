using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superman : Person<Eye>,Hear
{
    public void Init()
    {
    }

    public void Init1()
    {
    }
}

public class Person<T> where T :Hear
{
    void InitPerson()
    {

    }
}
public interface Hear
{
    void Init1();
}
public interface Eye:Hear
{
    void Init2();
}
public interface Gender
{
    void Init3();
}
