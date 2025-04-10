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
    public class Torus : ModelVisual3D
    {
        public Torus()
        {
            Draw(_radius, _thickness);
        }
        private double _radius;
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Draw(_radius, _thickness);
            }
        }
        private double _thickness;
        public double Thickness
        {
            get => _thickness;
            set
            {
                _thickness = value;
                Draw(_radius, _thickness);
            }
        }
        private Brush _color;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                Draw(_radius, _thickness);
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
        private void Draw(double radius, double thickness)
        {
            Point3D[] points = new Point3D[64 * 64];
            for (int i = 0; i < 64; i++)
            {
                double cosX = Math.Cos(Math.PI / 32 * i);
                double sinZ = Math.Sin(Math.PI / 32 * i);
                for (int j = 0; j < 64; j++)
                    points[i * 64 + j] = new Point3D(cosX * (radius + Math.Cos(Math.PI / 32 * j) * thickness), Math.Sin(Math.PI / 32 * j) * thickness, sinZ * (radius + Math.Cos(Math.PI / 32 * j) * thickness));
            }
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            DiffuseMaterial material = new DiffuseMaterial(RandBrush());
            for (int i = 0; i < 64; i++)
                for (int j = 0; j < 64; j++)
                {
                    GeometryModel3D face = AddFace(new Point3D[]
                    {
                        points[i * 64 + j],
                        points[i * 64 + (j+1) % 64],
                        points[(i+1) % 64 * 64 + (j+1) % 64],
                        points[(i+1) % 64 * 64 + j]
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
