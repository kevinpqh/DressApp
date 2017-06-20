using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace DressApp.Model.ClothingItems
{
    class TieItem : ClothingItemBase
    {
        // constructor TieItem

        public TieItem(Model3DGroup model, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.ShoulderCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = JointType.Spine;
        }
    }
}
