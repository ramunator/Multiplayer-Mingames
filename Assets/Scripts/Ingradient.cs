using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIngradiant
{
    public enum Ingradient
    {
        peperoni
    };

    Ingradient Ingradiant { get; set; }

    public void PlaceIngradiant();
}
