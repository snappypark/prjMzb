using UnityEngine;

public class npc : unit
{
    [HideInInspector] public attrib2_ attb2 = new attrib2_();
  
    public class attrib2_
    {
        public float maxPathSqrDist = maxPathSqr170; // 13 => 169
        public float moveSpeed = 3.2f; // 4.5f
        
    }
    
    public static int maxPathSqr170 = 170; // 13
    public static int maxPathSqr180 = 180;
    public static int maxPathSqr190 = 190;
    public static int maxPathSqr200 = 200;
    public static int maxPathSqr230 = 230;
    public static int maxPathSqr270 = 270;
}
