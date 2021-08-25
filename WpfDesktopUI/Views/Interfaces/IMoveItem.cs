using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Views.Interfaces
{
    public interface IMoveItem
    {
        bool CanMoveUp { get; }
        bool CanMoveDown { get; }

        void MoveUp();      
        void MoveDown();
    }
}
