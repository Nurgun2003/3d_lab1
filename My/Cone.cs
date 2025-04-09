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
    public class Cone : ModelVisual3D
    {
        public Cone()
        {
            Draw(_radius, _height);
        }
        private double _radius;
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Draw(_radius, _height);
            }
        }
        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                Draw(_radius, _height);
            }
        }
        private Brush _color;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                Draw(_radius, _height);
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
        private void Draw(double radius, double height)
        {
            Point3D[] points = new Point3D[65];
            points[0] = new Point3D(0, height, 0);
            for (int i = 1; i < points.Length; i++)
                points[i] = new Point3D(Math.Cos(Math.PI / 32 * i) * radius, 0, Math.Sin(Math.PI / 32 * i) * radius);
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            // Основание
            DiffuseMaterial material = new DiffuseMaterial(RandBrush());
            GeometryModel3D face = AddFace(points.Skip(1).ToArray(), material);
            m3dg.Children.Add(face);
            // Боковые грани
            material = new DiffuseMaterial(RandBrush());
            for (int i = 1; i < points.Length; i++)
            {
                face = AddFace
                (
                    new Point3D[]
                    {
                        points[i],
                        points[0],
                        points[i+1 <= 64 ? i+1 : 1]
                    },
                    material
                );
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
