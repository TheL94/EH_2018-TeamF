using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IGetSlower
    {
        bool IsSlowed { get; set; }
        float MovementSpeed { get; set; }
    }
}