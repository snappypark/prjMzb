using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nj
{
    public class qObj : cObj
    {
        public virtual void Assign(cel1l cell) { }
        public virtual void UnAssign() { }


        public virtual void AssignWall(zone.wall_ info, Material mini, Material top, Material sideUp, Material sideDown)
        {
        }
        
    }
}