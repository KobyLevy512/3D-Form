using static ExtendMath.EMath;

namespace Raycasting2021
{
    public static class Camera
    {
        static float yaw = 0; 
        static float fov = 0; 
        static float aspect = 0; 
        static float far = 0; 
        static float near = 0; 

        public static Matrix3x3 View = RotationMatrix(0,0,0);
        public static Vector3 Location = new Vector3();
        public static Vector3 Projection = ProjectionVector(1f, 0.8f, 1000, 0.1f);

        public static float Yaw
        {
            get => yaw;
            set
            {
                yaw = value;
                View = RotationMatrix(0,yaw,0);
            }
        }
        public static float Fov
        {
            get => fov;
            set
            {
                fov = value;
                Projection = ProjectionVector(fov, aspect, far, near);
            }
        }
        public static float Aspect
        {
            get => aspect;
            set
            {
                aspect = value;
                Projection = ProjectionVector(fov, aspect, far, near);
            }
        }
        public static float Far
        {
            get => far;
            set
            {
                far = value;
                Projection = ProjectionVector(fov, aspect, far, near);
            }
        }
        public static float Near
        {
            get => near;
            set
            {
                near = value;
                Projection = ProjectionVector(fov, aspect, far, near);
            }
        }
    }
}
