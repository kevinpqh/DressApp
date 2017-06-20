using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace DressApp.Model.ClothingItems
{
    class BagItem : ClothingItemBase
    {
        /// Constructores de BagItem

        public BagItem(Model3DGroup model, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.HandLeft;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = JointType.HipCenter;
        }
    }
}
