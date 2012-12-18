using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Client.Views.CustomControls;

namespace MonkeyMusicCloud.Client.Views
{
    public partial class PlayerView : UserControl
    {
        public PlayerView()
        {
            InitializeComponent();
        }
        
        private void Slider_OnThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            CallStartDragCommand();
        }

        private void CallStartDragCommand()
        {
            ((PlayerViewModel) DataContext).StartDragCommand.Execute(null);
        }

        private void Slider_OnThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            CallStopDragCommand(sender);
        }

        private void CallStopDragCommand(object sender)
        {
            SliderWithDraggingEvents slider = (SliderWithDraggingEvents) sender;
            ((PlayerViewModel) DataContext).StopDragCommand.Execute(slider.Value);
        }

        private void Slider_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            CallStartDragCommand();
        }

        private void Slider_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            CallStopDragCommand(sender);
        }
    }
}