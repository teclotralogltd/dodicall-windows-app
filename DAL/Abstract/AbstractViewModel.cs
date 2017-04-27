//
//  Copyright (C) 2016, Telco Cloud Trading & Logistic Ltd
//
//  This file is part of dodicall.
//  dodicall is free software : you can redistribute it and / or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  dodicall is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with dodicall.If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using DAL.ViewModel;

namespace DAL.Abstract
{
    public abstract class AbstractViewModel : AbstractLocalization
    {
        // _dispatcher нужен для применения события DoCallback, т.к. он вызывается в другом потоке и объекты,
        // созданные в нем, при биндинге заваливают приложение из-за ошибки доступа к UI
        // (нельзя в коллекции прибинденные в UI добавлять объекты созданные в другом потоке,
        // потому что DependencyObject и DependencySource должны пренадлежать одному потоку)

        /// <summary> Dispatcher потока в котором создается объект </summary>
        protected readonly Dispatcher CurrentDispatcher = Dispatcher.FromThread(Thread.CurrentThread);

        /// <summary> Событие блокировки экрана </summary>
        public event EventHandler<bool> LockUI;

        /// <summary> Инвокатор события LockUI </summary>
        protected void OnLockUI(bool lockUI)
        {
            LockUI?.Invoke(this, lockUI);
        }

        /// <summary> Событие закрытия View </summary>
        public event EventHandler CloseView;

        /// <summary> Инвокатор события CloseView </summary>
        protected void OnCloseView()
        {
            CloseView?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Событие ViewModel </summary>
        public event EventHandler<ViewModelEventHandlerArgs> EventViewModel;

        /// <summary> Инвокатор события EventViewModel </summary>
        protected void OnEventViewModel(string key, object data)
        {
            EventViewModel?.Invoke(this, new ViewModelEventHandlerArgs {Key = key, Data = data});
        }

        /// <summary> Инвокатор события EventViewModel </summary>
        protected void OnEventViewModel(string key)
        {
            EventViewModel?.Invoke(this, new ViewModelEventHandlerArgs { Key = key });
        }

        /// <summary> Команда ComingSoon </summary>
        public Command CommandComingSoon { get; set; }

        /// <summary> Конструктор </summary>
        protected AbstractViewModel()
        {
            CommandComingSoon = new Command(obj => OnEventViewModel("ComingSoon"));
        }
    }
}
