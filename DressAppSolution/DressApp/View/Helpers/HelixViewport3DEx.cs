using System;
using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace DressApp.View.Helpers

{
    public class HelixViewport3DEx : HelixViewport3D
    {
        #region Private Fields

        // matriz3D a cero

        private Matrix3D _zeroMatrix = new Matrix3D(0, 0, 0, 0
                                                 , 0, 0, 0, 0
                                                 , 0, 0, 0, 0
                                                 , 0, 0, 0, 0);
        #endregion Private Fields
        #region Dependency Properties

        // Propiedades de ViewportTransform
 
        public static readonly DependencyProperty ViewportTransformProperty = DependencyProperty.RegisterAttached(
            "ViewportTransform", typeof(Matrix3D), typeof(HelixViewport3DEx), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty CameraTransformProperty = DependencyProperty.RegisterAttached(
            "CameraTransform", typeof(Matrix3D), typeof(HelixViewport3DEx), new FrameworkPropertyMetadata(null));
        #endregion Dependency Properties
        #region Public Properties

        // Gets y sets de CameraTransform.

        public Matrix3D CameraTransform
        {
            get { return (Matrix3D)GetValue(CameraTransformProperty); }
            set { SetValue(CameraTransformProperty, value); }
        }

        // Gets y sets de viewportTransform.

        public Matrix3D ViewportTransform
        {
            get { return (Matrix3D)GetValue(ViewportTransformProperty); }
            set { SetValue(ViewportTransformProperty, value); }
        }
        #endregion Public Properties
        #region Private Methods

        // GetViewportTransform - Obtener el objeto

        private Matrix3D GetViewportTransform()
        {
            return new Matrix3D(Viewport.ActualWidth / 2, 0, 0, 0
                              , 0, -Viewport.ActualHeight / 2, 0, 0
                              , 0, 0, 1, 0
                              , Viewport.ActualWidth / 2
                              , Viewport.ActualHeight / 2, 0, 1);
        }

        ///GetCameraTransform - obtener el objeto
        // retorna: Matriz que representa la cameraTransform

        public Matrix3D GetCameraTransform()
        {
            Matrix3D matx = Matrix3D.Identity;
            if (Camera.Transform != null)
            {
                Matrix3D matxCameraTransform = Camera.Transform.Value;

                if (!matxCameraTransform.HasInverse)
                    return _zeroMatrix;
                matxCameraTransform.Invert();
                matx.Append(matxCameraTransform);
            }

            matx.Append(GetViewMatrix());
            matx.Append(GetProjectionMatrix());
            return matx;
        }

        // GetViewMatrix - obtener el objeto
        // retorna: Matriz que representa la GetViewMatrix
     

        public Matrix3D GetViewMatrix()
        {
            Vector3D z = -Camera.LookDirection;
            z.Normalize();

            Vector3D x = Vector3D.CrossProduct(Camera.UpDirection, z);
            x.Normalize();

            Vector3D y = Vector3D.CrossProduct(z, x);
            Vector3D position = (Vector3D)Camera.Position;

            return new Matrix3D(x.X, y.X, z.X, 0
                              , x.Y, y.Y, z.Y, 0
                              , x.Z, y.Z, z.Z, 0
                              , -Vector3D.DotProduct(x, position)
                              , -Vector3D.DotProduct(y, position)
                              , -Vector3D.DotProduct(z, position), 1);
        }
        
        // GetProjectionMatrix- obtener el objeto
        // retorna: Matriz que representa la GetProjectionMatrix

        private Matrix3D GetProjectionMatrix()
        {
            double aspectRatio = Viewport.ActualWidth / Viewport.ActualHeight;

            double x = 2 / ((OrthographicCamera)Camera).Width;
            double y = x * aspectRatio;
            double near = Camera.NearPlaneDistance;
            double far = Camera.FarPlaneDistance;

            if (Double.IsPositiveInfinity(far))
                far = 1E10;

            return new Matrix3D(x, 0, 0, 0
                              , 0, y, 0, 0
                              , 0, 0, 1 / (near - far), 0
                              , 0, 0, near / (near - far), 1);
        }
        #endregion Private Methods
        #region Public Methods

        // SetTransformMatrix.

        public void SetTransformMatrix()
        {
            SetCurrentValue(ViewportTransformProperty, GetViewportTransform());
            SetCurrentValue(CameraTransformProperty, GetCameraTransform());
        }
        #endregion Public Methods
    }
}
