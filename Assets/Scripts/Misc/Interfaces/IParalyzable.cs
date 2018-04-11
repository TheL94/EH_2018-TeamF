using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IParalyzable
    {
        bool IsParalized { get; set; }
        void SetParalisys(bool _isParalized);
    }
}