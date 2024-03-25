using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleName
{
	public class MiamiIslandsParallax : BackgroundParalax
	{
    public float[] offsets;
    
    protected override float getExtraOffset(int id)
    {
        return offsets[id];
    }
  }
}