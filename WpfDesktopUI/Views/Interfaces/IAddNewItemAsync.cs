﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Views.Interfaces
{
    public interface IAddNewItemAsync
    {
        bool CanAddNew { get; }
        Task AddNew();
    }
}
