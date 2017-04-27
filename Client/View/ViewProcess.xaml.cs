using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewProcess.xaml
    /// </summary>
    public partial class ViewProcess : UserControl
    {
        /// <summary> Элемент анимации </summary>
        private readonly Storyboard _storyboardRotationImage;

        /// <summary> Dispatcher потока в котором создается объект </summary>
        private readonly Dispatcher _dispatcher;

        /// <summary> Конструктор </summary>
        public ViewProcess()
        {
            InitializeComponent();

            _dispatcher = Dispatcher.FromThread(Thread.CurrentThread);

            _storyboardRotationImage = GridMain.Resources[@"StoryboardRotation"] as Storyboard;
        }

        /// <summary> Запустить анимацию ожидания </summary>
        public void WaitingAnimationStart()
        {
            _dispatcher.BeginInvoke(new Action(AnimationStart));
        }

        /// <summary> Остановить анимацию ожидания </summary>
        public void WaitingAnimationStop()
        {
            _dispatcher.BeginInvoke(new Action(AnimationStop));
        }

        /// <summary> Запустить анимацию </summary>
        private void AnimationStart()
        {
            Visibility = Visibility.Visible;

            _storyboardRotationImage?.Begin();
        }

        /// <summary> Остановить анимацию </summary>
        private void AnimationStop()
        {
            Visibility = Visibility.Hidden;

            _storyboardRotationImage?.Stop();
        }
    }
}
