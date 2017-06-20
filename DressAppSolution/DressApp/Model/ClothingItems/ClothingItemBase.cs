using System;
using System.Windows;
using System.Windows.Media.Media3D;
using DressApp.ViewModel;
using Microsoft.Kinect;

namespace DressApp.Model.ClothingItems
{
    public abstract class ClothingItemBase : ViewModelBase
    {
        #region Atributos Protegidos
        
        // Tolerancia
        protected double Tolerance;
        #endregion

        #region Atributos Privados
        
        // escalas del modelo
        private double _heightScale;
        
        private double _widthScale;

        // determina la medida del model.
        private double _heightModelScale;
        
        private double _widthModelScale;
        
        // model
        private Model3DGroup _model;

        // _basicBounds del model
        private Rect3D _basicBounds;

        #endregion

        #region Propiedades Publicas
        
        // Gets y sets angulo.
        public double Angle { get; set; }
        
        // Gets matrix para scaleTransform.
        public Transform3D ScaleTransformation { get; protected set; }
        
        // Gets y sets model.
        public Model3DGroup Model
        {
            get { return _model; }
            set
            {
                if (_model == value)
                    return;
                _model = value;
                OnPropertyChanged("Model");
            }
        }
        
        // Gets y sets  height
        public double HeightScale
        {
            get { return _heightScale; }
            set
            {
                if (_heightScale == value)
                    return;
                _heightScale = value;
                SetScaleTransformation();
            }
        }
        
        // Gets y sets width 
        
        public double WidthScale
        {
            get { return _widthScale; }
            set
            {
                if (_widthScale == value)
                    return;
                _widthScale = value;
                SetScaleTransformation();
            }
        }
        
        // Gets y sets Y.

        public double DeltaPosition { get; set; }

        // Gets y sets para JointToTrackPosition

        public JointType JointToTrackPosition { get; protected set; }

        // Gets y sets LeftJointToTrackAngle        

        public JointType LeftJointToTrackAngle { get; protected set; }

        // Gets y sets RightJointToTrackAngle

        public JointType RightJointToTrackAngle { get; protected set; }

        // Gets y sets LeftJointToTrackScale

        public JointType LeftJointToTrackScale { get; protected set; }

        // Gets y sets RightJointToTrackScale

        public JointType RightJointToTrackScale { get; protected set; }
        #endregion

        #region Constructor
        
        // Constructor de ClothingItemBase
 
        protected ClothingItemBase(Model3DGroup model, double tolerance, double deltaPosition)
        {
            Model = model;
            _basicBounds = model.Bounds;
            Tolerance = tolerance;
            _widthScale = _heightScale = 1;
            DeltaPosition = deltaPosition;
        }
        #endregion

        #region Metodos Publicos
        
        // actualizar item position

        public void UpdateItemPosition(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            if (skeleton == null || skeleton.Joints[LeftJointToTrackAngle].TrackingState == JointTrackingState.NotTracked
                || skeleton.Joints[RightJointToTrackAngle].TrackingState == JointTrackingState.NotTracked
                || skeleton.Joints[JointToTrackPosition].TrackingState == JointTrackingState.NotTracked)
                return;

            TrackSkeletonParts(skeleton, sensor, width, height);
        }
        #endregion

        #region Metodos Privados
        
        // Set position for part of set
 
        private void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            Angle = TrackJointsRotation(sensor, skeleton.Joints[LeftJointToTrackAngle], skeleton.Joints[RightJointToTrackAngle]);

            var joint = KinectService.GetJointPoint(skeleton.Joints[JointToTrackPosition], sensor, width, height);
            var point3D = Point2DtoPoint3D(new Point(joint.X, joint.Y * DeltaPosition));

            FitModelToBody(skeleton.Joints[LeftJointToTrackScale], skeleton.Joints[RightJointToTrackScale], sensor, width, height);

            var transform = new Transform3DGroup();
            transform.Children.Add(ScaleTransformation);
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Angle)));
            transform.Children.Add(new TranslateTransform3D(point3D.X, point3D.Y, point3D.Z));
            Model.Transform = transform;
        }
        
        // tranformar puntos de 2D a 3D.
        
        private Point3D Point2DtoPoint3D(Point point2D)
        {
            Point3D point = new Point3D(point2D.X, point2D.Y, 0);
            Matrix3D matxViewport = ClothingManager.Instance.ViewportTransform;
            Matrix3D matxCamera = ClothingManager.Instance.CameraTransform;

            try
            {
                matxViewport.Invert();
                matxCamera.Invert();
            }
            catch (Exception)
            {
                return new Point3D(0, 0, 0);
            }

            Point3D pointNormalized = matxViewport.Transform(point);
            pointNormalized.Z = 0.01;
            Point3D pointNear = matxCamera.Transform(pointNormalized);
            pointNormalized.Z = 0.99;
            Point3D pointFar = matxCamera.Transform(pointNormalized);

            double factor = (0 - pointNear.Z) / (pointFar.Z - pointNear.Z);
            double x = pointNear.X + factor * (pointFar.X - pointNear.X);
            double y = pointNear.Y + factor * (pointFar.Y - pointNear.Y);
            return new Point3D(x, y, 0);
        }
        
        // Sets ScnaleTransformation.
        
        private void SetScaleTransformation()
        {
            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new ScaleTransform3D(_widthScale * _widthModelScale, 1, _widthScale * _widthModelScale));
            transform.Children.Add(new ScaleTransform3D(1, _heightScale * _heightModelScale, 1));
            ScaleTransformation = transform;
        }
        
        // transforamr 3D a 2D.

        private Point Point3DtoPoint2D(Point3D point)
        {
            Matrix3D matrix = ClothingManager.Instance.CameraTransform;
            matrix.Append(ClothingManager.Instance.ViewportTransform);

            Point3D pointTransformed = matrix.Transform(point);
            return new Point(pointTransformed.X, pointTransformed.Y);
        }
        

        private double TrackJointsRotation(KinectSensor sensor, Joint leftJoint, Joint rightJoint)
        {
            if (leftJoint.TrackingState == JointTrackingState.NotTracked
               || rightJoint.TrackingState == JointTrackingState.NotTracked)
                return double.NaN;

            var jointLeftPosition = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(leftJoint.Position, sensor.DepthStream.Format);
            var jointRightPosition = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(rightJoint.Position, sensor.DepthStream.Format);

            return -(Math.Atan(((double)jointLeftPosition.Depth - jointRightPosition.Depth)
                / ((double)jointRightPosition.X - jointLeftPosition.X)) * 180.0 / Math.PI);
        }
        

        private void FitModelToBody(Joint joint1, Joint joint2, KinectSensor sensor, double width, double height)
        {
            if (joint1.TrackingState == JointTrackingState.NotTracked
                || joint2.TrackingState == JointTrackingState.NotTracked)
                return;

            var joint1Position = KinectService.GetJointPoint(joint1, sensor, width, height);
            var joint2Position = KinectService.GetJointPoint(joint2, sensor, width, height);

            var location = _basicBounds.Location;
            Point leftBound = Point3DtoPoint2D(location);
            Point rightBound =
                Point3DtoPoint2D(new Point3D(location.X + _basicBounds.SizeX, location.Y + _basicBounds.SizeY
                    , location.Z + _basicBounds.SizeZ));

            double ratio = (Math.Abs(joint1Position.Y - joint2Position.Y) / Math.Abs(leftBound.Y - rightBound.Y));
            _widthModelScale = _heightModelScale = ratio * Tolerance;
            SetScaleTransformation();
        }
        #endregion

        #region Enums
        public enum ClothingType
        {
            HatItem,
            SkirtItem,
            GlassesItem,
            DressItem,
            TieItem,
            BagItem,
            TopItem
        }

        public enum MaleFemaleType
        {
            Male,
            Female,
            Both
        }
        #endregion Enums
    }
}
