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
            Slider.ApplyTemplate();

            Track track = Slider.Template.FindName("PART_Track", Slider) as Track;
            if (track != null)
            {
                Thumb thumb = track.Thumb;

                thumb.MouseEnter += thumb_MouseEnter;
            }
        }

        private void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.MouseDevice.Captured == null)
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);

                args.RoutedEvent = MouseLeftButtonDownEvent;

                Thumb thumb = sender as Thumb;
                if (thumb != null) thumb.RaiseEvent(args);
            }
        }

        private void Slider_OnThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            CallStartDragCommand();
        }
        
        private void Slider_OnThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            CallStopDragCommand(sender);
        }
        
        private void Slider_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            CallStartDragCommand();
        }

        private void CallStartDragCommand()
        {
            ((PlayerViewModel)DataContext).StartDragCommand.Execute(null);
        }

        private void CallStopDragCommand(object sender)
        {
            SliderWithDraggingEvents slider = (SliderWithDraggingEvents)sender;
            ((PlayerViewModel)DataContext).StopDragCommand.Execute(slider.Value);
        }

    }
}