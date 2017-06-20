using DressApp.ViewModel;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DressApp.Model
{
    public class SkeletonManager : ViewModelBase
    {
        #region Prpiedades publicas


        public ObservableCollection<Polyline> SkeletonParts
        {
            get { return _skeletonModels; }
            set
            {
                if (_skeletonModels == value)
                    return;
                if (value.Count > 0)
                {
                    _skeletonModels = value;
                    OnPropertyChanged("SkeletonParts");
                }
            }
        }
        #endregion

        #region Atributos Privados
        private ObservableCollection<Polyline> _skeletonModels;
        #endregion
        
        #region Metodos Publicos

        public void DrawSkeleton(Skeleton[] skeletons, Brush brush, KinectSensor sensor, double width, double height)
        {
            var skeletonModels = new ObservableCollection<Polyline>();
            foreach (var skeleton in skeletons.Where(skeleton => skeleton.TrackingState != SkeletonTrackingState.NotTracked))
            {
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateBody(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateLeftHand(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateRightHand(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateLeftLeg(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateRightLeg(), sensor, width, height));
            }
            SkeletonParts = skeletonModels;
        }
        #endregion

        #region Metodos Privados


        private IEnumerable<JointType> CreateBody()
        {
            return new[]
                        {
                            JointType.Head
                            , JointType.ShoulderCenter
                            , JointType.ShoulderLeft
                            , JointType.Spine
                            , JointType.ShoulderRight
                            , JointType.ShoulderCenter
                            , JointType.HipCenter
                            , JointType.HipLeft
                            , JointType.Spine
                            , JointType.HipRight
                            , JointType.HipCenter
                        };
        }

        private IEnumerable<JointType> CreateRightHand()
        {
            return new[]
                        {
                            JointType.ShoulderRight
                            , JointType.ElbowRight
                            , JointType.WristRight
                            , JointType.HandRight
                        };
        }


        private IEnumerable<JointType> CreateLeftHand()
        {
            return new[]
                        {
                            JointType.ShoulderLeft
                            , JointType.ElbowLeft
                            , JointType.WristLeft
                            , JointType.HandLeft
                        };
        }

        private IEnumerable<JointType> CreateRightLeg()
        {
            return new[]
                        {
                            JointType.HipRight
                            , JointType.KneeRight
                            , JointType.AnkleRight
                            , JointType.FootRight
                        };
        }


        private IEnumerable<JointType> CreateLeftLeg()
        {
            return new[]
                        {
                            JointType.HipLeft
                            , JointType.KneeLeft
                            , JointType.AnkleLeft
                            , JointType.FootLeft
                        };
        }


        private Polyline CreateFigure(Skeleton skeleton, Brush brush, IEnumerable<JointType> joints
            , KinectSensor sensor, double width, double height)
        {
            var figure = new Polyline { StrokeThickness = 8, Stroke = brush };

            foreach (var joint in joints)
            {
                var jointPoint = KinectService.GetJointPoint(skeleton.Joints[joint], sensor, width, height);
                figure.Points.Add(new Point(jointPoint.X, jointPoint.Y));
            }

            return figure;
        }
        #endregion
    }
}
