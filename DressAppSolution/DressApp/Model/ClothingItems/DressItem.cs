using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace DressApp.Model.ClothingItems
{
    class DressItem : ClothingItemBase
    {
        #region ctor

        // Constructor de DressObject

        public DressItem(Model3DGroup model, JointType bottomJoint, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = bottomJoint;
        }
        #endregion ctor
    }
}
