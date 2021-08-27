using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Views.Interfaces.Composite
{
    public interface IAddView : IHasItems, IAddNewItem, IAddSelectedItem, IRemoveItem,
        IHasError, IMoveItem, IGoBack
    {
    }
}
