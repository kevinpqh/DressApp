using KinectFittingRoom.View.Buttons.Events;
using System.Windows.Threading;

namespace KinectFittingRoom.View.Buttons
{
    
    // button repsonde al eventos del Kincect 
    
    class KinectRepeatableButton : KinectButton
    {
        #region Protected Methods
        
        // Imitia eveneto click para KinectSizeButton
        
        protected override void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            SetValue(IsClickedProperty, true);
            AfterClickTimer.Start();
        }
        
        // Reset contadores
        
        protected override void ResetTimer(DispatcherTimer timer)
        {
            timer.Stop();
            if (timer == ClickTimer)
                ClickTicks = 0;
            else
            {
                AfterClickTicks = 0;
                if (HandIsOverButton)
                    ClickTimer.Start();
            }
        }
        #endregion Protected Methods
    }
}
