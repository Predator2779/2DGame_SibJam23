using Character.Item;
using UnityEngine;

namespace Character.Health
{
    public class TearProcessor : HealthProcessor
    {
        public override void ResponseAction(GameObject g)
        {
            if (g.TryGetComponent(out Tear tear))
                TakeHeal(tear.TearPoints);
        }
    }
}