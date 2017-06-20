using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace DressApp.Model.ClothingItems
{
    class HatItem : ClothingItemBase
    {

        //constructor de HatItem

        public HatItem(Model3DGroup model, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.Head;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.Head;
            RightJointToTrackScale = JointType.ShoulderCenter;
        }
    }
}
