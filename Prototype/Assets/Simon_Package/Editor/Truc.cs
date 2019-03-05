using UnityEngine;
using System.Collections.Generic;

public class Liquide
{
    private Color color;
    protected int size;

    protected virtual void Get()
    {

    }
}

public class Boisson
{
    public int price;
    public int index;

    public Boisson()
    {

    }


}

public class Distributeur
{
    List<Boisson> boissons;
    bool[] motors;

    private void RotateMotor(int index)
    {
        motors[index] = true;
    }

    public void AddMoney(int value)
    {
        RotateMotor(0);
    }
}

public class ObjectB
{
    public Object[] Objects { get; private set; }

    public void SetMoving(bool value)
    {

    }
}

public class ObjectC
{
    public ObjectB objectB;

    void Update()
    {
        objectB.SetMoving(true);
    }
}
