using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalItem
{
    public interface IClass
    {
        Item GetInstance();
        void SetInstance(Item i);
    }
}
