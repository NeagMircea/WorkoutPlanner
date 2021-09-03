using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Models
{
    public interface IDisplayModel
    {
        int GetId { get; }
        int GetOrder { get; }
    }
}
