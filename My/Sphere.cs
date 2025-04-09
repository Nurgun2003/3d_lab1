using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace lab1.My
{
    public class Sphere : ModelVisual3D
    {
        public Sphere()
        {
            Draw(_radius);
        }
        private double _radius;
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Draw(_radius);
            }
        }
        private Brush _color;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                Draw(_radius);
            }
        }

        //Добавление грани
        private static GeometryModel3D AddFace(Point3D[] points, Material material)
        {
            Point3DCollection positions = new Point3DCollection();
            for (int i = 2; i < points.Length; i++)
            {
                positions.Add(points[0]);
                positions.Add(points[i - 1]);
                positions.Add(points[i]);
            }
            GeometryModel3D geometryModel3D = new GeometryModel3D()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = positions
                },
                Material = material
            };
            return geometryModel3D;
        }
        private void Draw(double radius)
        {
            Point3D[] points = new Point3D[64 * 63 + 2];
            points[0] = new Point3D(0, radius, 0);
            for (int i = 1; i < 64; i++)
                for (int j = 0; j < 64; j++)
                    points[i * 64 - 63 + j] = new Point3D(Math.Cos(Math.PI / 32 * j) * Math.Sin(Math.PI / 32 * i) * radius, Math.Cos(Math.PI / 32 * i) * radius, Math.Sin(Math.PI / 32 * j) * Math.Sin(Math.PI / 32 * i) * radius);
            points[points.Length - 1] = new Point3D(0, -radius, 0);
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            DiffuseMaterial material = new DiffuseMaterial(RandBrush());
            for (int i = 1; i <= 64; i++)
            {
                GeometryModel3D face = AddFace(new Point3D[]
                {
                    points[i],
                    points[0],
                    points[i+1 <= 64 ? i+1 : 1]
                }, material);
                m3dg.Children.Add(face);
                face = AddFace(new Point3D[]
                {
                    points[62 * 64 + i],
                    points[62 * 64 + (i+1 <= 64 ? i+1 : 1)],
                    points[points.Length - 1]
                }, material);
                m3dg.Children.Add(face);
            }
            for (int i = 2; i < 64; i++)
                for (int j = 0; j < 64; j++)
                {
                    GeometryModel3D face = AddFace(new Point3D[]
                    {
                        points[i * 64 - 63 + j],
                        points[(i-1) * 64 - 63 + j],
                        points[(i-1) * 64 - 63 + (j+1 < 64 ? j+1 : 0)],
                        points[i * 64 - 63 + (j+1 < 64 ? j+1 : 0)]
                    }, material);
                    m3dg.Children.Add(face);
                }
            //Сохранение данных объекта
            Content = m3dg;
        }
        private Random rnd = new Random();
        private Brush RandBrush()
        {
            if (Color != null)
                return Color;
            return new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)rnd.Next(), (byte)rnd.Next(), (byte)rnd.Next()));
        }
    }
}
