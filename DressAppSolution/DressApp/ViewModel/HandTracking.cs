using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Windows;

namespace DressApp.ViewModel
{
    public class HandTracking : ViewModelBase
    {
        #region Atributos Privados
        // Posicion de la mano Izquierda
        private Point _leftPosition;
        // Posicion de la mano Derecha
        private Point _rightPosition;
        #endregion

        #region Propiedades Publicas
        //Gets o sets de la posicion de la mano izquierda
        public Point LeftPosition
        {
            get { return _leftPosition; }
            set
            {
                if (_leftPosition == value)
                    return;
                _leftPosition = value;
                OnPropertyChanged("LeftPosition");
            }
        }
        // Gets o sets de la posicion de la mano izquierda
        public Point RightPosition
        {
            get { return _rightPosition; }
            set
            {
                if (_rightPosition == value)
                    return;
                _rightPosition = value;
                OnPropertyChanged("RightPosition");
            }
        }
        #endregion

        #region Metodos
        // invoca la posición de la mano del esqueleto si no es nulo
        public void UpdateHandCursor(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            if (skeleton == null) return;

            TrackHand(skeleton.Joints[JointType.HandLeft], skeleton.Joints[JointType.HandRight], sensor, width, height);
        }
        
        //las coordenadas de la mano izquierda y derecha en el espacio -- kinect image ancho y altura
        private void TrackHand(Joint leftHand, Joint rightHand, KinectSensor sensor, double width, double height)
        {
            if (leftHand.TrackingState == JointTrackingState.NotTracked && rightHand.TrackingState == JointTrackingState.NotTracked)
                return;

            DepthImagePoint leftPoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(leftHand.Position
                , sensor.DepthStream.Format);
            int lx = (int)((leftPoint.X * width / sensor.DepthStream.FrameWidth));
            int ly = (int)((leftPoint.Y * height / sensor.DepthStream.FrameHeight));

            DepthImagePoint rightPoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(rightHand.Position
                , sensor.DepthStream.Format);
            int rx = (int)((rightPoint.X * width / sensor.DepthStream.FrameWidth));
            int ry = (int)((rightPoint.Y * height / sensor.DepthStream.FrameHeight));

            LeftPosition = new Point(lx, ly);
            RightPosition = new Point(rx, ry);
        }
        #endregion
    }
}
