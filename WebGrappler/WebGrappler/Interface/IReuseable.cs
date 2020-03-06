using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGrappler.Interface
{
    interface IReuseable
    {
        void ChangeReuseable(bool reuseable);
        bool CanReuse();
        void Reset();
        void Release();
    }
}
