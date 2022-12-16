using UnityEngine;

namespace Tools
{
    public static class MaskHelper
    {
        public static bool CheckIfLayerInMask(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}
