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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Model;
using DAL.WrapperBridge;

namespace DAL.ViewModel
{
    public class ViewModelContactDirectory : AbstractViewModel
    {
        ///<summary> Поток выполнения асинхронного поиска в директории </summary>
        private Thread _thread;

        ///<summary> Событие успешной отправки заявки в друзья </summary>
        public event EventHandler<ModelContact> SendRequestSuccessful;

        ///<summary> Объект BackgroundWorker для асинхронного поиска контактов </summary>
        private BackgroundWorker _backgroundWorker;

        ///<summary> Фильтр поиска </summary>
        private string _filter;

        ///<summary> Фильтр поиска </summary>
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter == value) return;
                _filter = value;
                OnPropertyChanged("Filter");

                // закоментированное нужно для поиска только по номеру начиная с 6 цифры
                // т.к. было решено вернуть первоначальный вариант для поиска по короткому d-sip, то закоментировал
                // но пока удалять не стал

                //var regex = new Regex(@"\D"); // Соответствует любая не цифра
                //var match = regex.Match(_filter);

                //var length = match.Success ? 2 : 5;

                if (_backgroundWorker != null && _backgroundWorker.IsBusy)
                {
                    _thread.Abort();

                    _backgroundWorker.Dispose(); // хз, может и не нужно, но на всякий случай

                    OnLockUI(false);
                }

                if (_filter.Trim().Length > 2 /*length*/)
                {
                    FindModelContactsInDirectory();
                }
                else
                {
                    ListModelContact = null;
                }
            }
        }

        ///<summary> Список контактов </summary>
        private void FindModelContactsInDirectory()
        {
            _backgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = true };

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;

            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;

            OnLockUI(true);

            _backgroundWorker.RunWorkerAsync();
        }

        ///<summary> Список контактов </summary>
        private List<ModelContact> _listModelContact;

        ///<summary> Список контактов </summary>
        public List<ModelContact> ListModelContact
        {
            get { return _listModelContact; }
            set
            {
                if (_listModelContact == value) return;
                _listModelContact = value;
                OnPropertyChanged("ListModelContact");
            }
        }

        ///<summary> Команда отправки запроса контакту </summary>
        public Command CommandSendRequest { get; set; }

        ///<summary> Конструктор </summary>
        public ViewModelContactDirectory()
        {
            CommandSendRequest = new Command(obj => SendRequest(obj as ModelContact));
        }

        ///<summary> Метод выполнения потока BackgroundWorker </summary>
        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            _thread = Thread.CurrentThread;

            var listModelContact = DataSourceContact.FindModelContactsInDirectory(_filter);

            e.Result = listModelContact;

            if (_backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        ///<summary> Метод завершения выполнения потока BackgroundWorker </summary>
        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnLockUI(false);

                throw e.Error;
            }
            else
            {
                var listModelContact = (List<ModelContact>)e.Result;

                ListModelContact = listModelContact.Select(obj => obj.GetDeepCopy()).ToList();

                OnLockUI(false);
            }
        }

        ///<summary> Метод отправки запроса контакту </summary>
        public void SendRequest(ModelContact modelContact)
        {
            if (modelContact == null) return;

            if (DataSourceContact.SendRequestModelContact(modelContact))
            {
                OnSendRequestSuccessful(modelContact);
            }
        }

        ///<summary> Инвокатор события SendRequestSuccessful </summary>
        private void OnSendRequestSuccessful(ModelContact modelContact)
        {
            SendRequestSuccessful?.Invoke(this, modelContact);
        }

        ///<summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            // тут пишем если что то нужно обновить
        }
    }
}
