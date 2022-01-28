using static ExtendMath.EMath;
using System.Drawing;
using System;

namespace Raycasting2021
{
    public class Cube
    {
        Triangle[] tris;
        public Vector3 Location;
        public Color Color;
        public Cube()
        {
            tris = new Triangle[8]
            {
                //Front
                new Triangle
                (
                    new Vector3(-1, 1, -1),
                    new Vector3(1, 1, -1),
                    new Vector3(1, -1, -1)
                ),
                new Triangle
                (
                    new Vector3(-1, 1, -1),
                    new Vector3(1, -1, -1),
                    new Vector3(-1, -1, -1)
                ),


                //Back
                new Triangle
                (
                    new Vector3(1, 1, 1),
                    new Vector3(-1, 1, 1),
                    new Vector3(-1, -1, 1)
                ),
                new Triangle
                (
                    new Vector3(1, 1, 1),
                    new Vector3(-1, -1, 1),
                    new Vector3(1, -1, 1)
                ),

                //Right
                new Triangle
                (
                    new Vector3(1, 1, -1),
                    new Vector3(1, 1, 1),
                    new Vector3(1, -1, 1)
                ),
                new Triangle
                (
                    new Vector3(1, 1, -1),
                    new Vector3(1, -1, 1),
                    new Vector3(1, -1, -1)
                ),

                //Left
                new Triangle
                (
                    new Vector3(-1, 1, 1),
                    new Vector3(-1, 1, -1),
                    new Vector3(-1, -1, -1)
                ),
                new Triangle
                (
                    new Vector3(-1, 1, 1),
                    new Vector3(-1, -1, -1),
                    new Vector3(-1, -1, 1)
                )
            };
            Color = Color.FromArgb(0, 0, 255);
            Location = new Vector3(0, 0, 5);
        }
        public void Draw(int screenW, int screenH, Graphics g)
        {
            int len = tris.Length;
            for(int i = 0; i< len; i++)
            {
                Triangle t = new Triangle(tris[i].V.V1, tris[i].V.V2, tris[i].V.V3);
                t.V += Location;
                t.V += Camera.Location;
                var nor = NormalSurface(t.V);
                if(Dot(nor, t.V.V1)<0.0f)
                {
                    t.V *= Camera.View;
                    Triangle[] clipped = TriangleAgainstClip(t);
                    int len2 = clipped.Length;
                    for(int j = 0; j< len2; j++)
                    {
                        clipped[j].V *= Camera.Projection;
                        float lig = Clamp(Dot(nor, new Vector3(0,0,-1)),0.5f,1);
                        byte red = (byte)(this.Color.R * lig);
                        byte green = (byte)(this.Color.G * lig);
                        byte blue = (byte)(this.Color.B * lig);
                        g.FillPolygon
                        (
                            new SolidBrush(Color.FromArgb(red, green, blue)),
                            new PointF[]
                            {
                                new PointF((clipped[j].V.M11 / clipped[j].V.M13 + 1) * screenW, (clipped[j].V.M12 / clipped[j].V.M13 + 1) * screenH),
                                new PointF((clipped[j].V.M21 / clipped[j].V.M23 + 1) * screenW, (clipped[j].V.M22 / clipped[j].V.M23 + 1) * screenH),
                                new PointF((clipped[j].V.M31 / clipped[j].V.M33 + 1) * screenW, (clipped[j].V.M32 / clipped[j].V.M33 + 1) * screenH)
                            }
                        );
                    }
                }
            }
        }
        public float Distance()
        {
            float z = 0, x = 0;
            if(Camera.Location.Z > 0)
            {
                if(Location.Z>0)
                {
                    z = Math.Abs(Camera.Location.Z - Location.Z);
                }
                else
                {
                    z = Location.Z + Camera.Location.Z;
                }
            }
            else
            {
                if (Location.Z > 0)
                {
                    z = Math.Abs(Camera.Location.Z - Location.Z);
                }
                else
                {
                    z = Math.Abs(Camera.Location.Z - Location.Z);
                }
            }
            if (Camera.Location.X > 0)
            {
                if (Location.X > 0)
                {
                    x = Math.Abs(Camera.Location.X - Location.X);
                }
                else
                {
                    x = Location.X + Camera.Location.X;
                }
            }
            else
            {
                if (Location.X > 0)
                {
                    x = Math.Abs(Camera.Location.X - Location.X);
                }
                else
                {
                    x = Math.Abs(Camera.Location.X + Location.X);
                }
            }
            return z + x;
        }
    }
}
