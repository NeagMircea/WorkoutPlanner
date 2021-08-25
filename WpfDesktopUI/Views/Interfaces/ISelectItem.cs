﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Views.Interfaces
{
    public interface ISelectItem
    {
        bool CanViewSelected { get; }
        Task ViewSelected();
    }
}
