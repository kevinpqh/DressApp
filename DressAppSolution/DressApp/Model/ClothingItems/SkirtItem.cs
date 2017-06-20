using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace DressApp.Model.ClothingItems
{
    class SkirtItem : ClothingItemBase
    {
        #region .ctor
        
        // Constructor de Skirt object

        public SkirtItem(Model3DGroup model, JointType bottomJoint, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.HipLeft;
            RightJointToTrackAngle = JointType.HipRight;
            LeftJointToTrackScale = JointType.HipCenter;
            RightJointToTrackScale = bottomJoint;
        }
        #endregion .ctor
    }
}
