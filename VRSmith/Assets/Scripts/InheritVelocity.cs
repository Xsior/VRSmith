using Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class InheritVelocity : MonoBehaviour
    {
        [SerializeField] private FloatVariable velocity;

        public float Velocity => velocity;
    }
}
