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
    public class TriangularPrism : ModelVisual3D
    {
        public TriangularPrism()
        {
            Draw(_point1, _point2, _point3, _height);
        }
        private Point3D _point1;
        public Point3D Point1
        {
            get => _point1;
            set
            {
                _point1 = value;
                Draw(_point1, _point2, _point3, _height);
            }
        }
        private Point3D _point2;
        public Point3D Point2
        {
            get => _point2;
            set
            {
                _point2 = value;
                Draw(_point1, _point2, _point3, _height);
            }
        }
        private Point3D _point3;
        public Point3D Point3
        {
            get => _point3;
            set
            {
                _point3 = value;
                Draw(_point1, _point2, _point3, _height);
            }
        }
        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                Draw(_point1, _point2, _point3, _height);
            }
        }
        private Brush _color;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                Draw(_point1, _point2, _point3, _height);
            }
        }

        //Добавление грани
        private static GeometryModel3D AddFace(
                        Point3D point1,
                        Point3D point2,
                        Point3D point3,
                        Material material)
        {
            GeometryModel3D geometryModel3D = new GeometryModel3D()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = new Point3DCollection()
                    {
                        point1,
                        point2,
                        point3, //вершины треугольника
                    }
                },
                Material = material //цвет грани
            };
            return geometryModel3D;
        }
        private static GeometryModel3D AddFace(
                        Point3D point1,
                        Point3D point2,
                        Point3D point3,
                        Point3D point4,
                        Material material)
        {
            GeometryModel3D geometryModel3D = new GeometryModel3D()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = new Point3DCollection()
                    {
                        point1,
                        point2,
                        point3, //вершины первого треугольника

                        point3,
                        point4,
                        point1 //вершины второго треугольника
                    }
                },
                Material = material //цвет грани
            };
            return geometryModel3D;
        }
        private void Draw(Point3D point1, Point3D point2, Point3D point3, double height)
        {
            double A = (point2.Y - point1.Y) * (point3.Z - point1.Z) - (point3.Y - point1.Y) * (point2.Z - point1.Z);
            double B = (point3.X - point1.X) * (point2.Z - point1.Z) - (point2.X - point1.X) * (point3.Z - point1.Z);
            double C = (point2.X - point1.X) * (point3.Y - point1.Y) - (point3.X - point1.X) * (point2.Y - point1.Y);
            double L = Math.Sqrt(A * A + B * B + C * C);
            Point3D N = new Point3D(A / L, B / L, C / L); //нормаль основания
            Point3D[] points =
            {
                point1,
                point2,
                point3,
                new Point3D(point1.X - N.X * height, point1.Y - N.Y * height, point1.Z - N.Z * height),
                new Point3D(point2.X - N.X * height, point2.Y - N.Y * height, point2.Z - N.Z * height),
                new Point3D(point3.X - N.X * height, point3.Y - N.Y * height, point3.Z - N.Z * height)
            };
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            // 1
            DiffuseMaterial material = new DiffuseMaterial(RandBrush());
            GeometryModel3D face = AddFace(points[0], points[1], points[2], material);
            m3dg.Children.Add(face);
            // 2
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[5], points[4], points[3], material);
            m3dg.Children.Add(face);
            // 3
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[3], points[4], points[1], points[0], material);
            m3dg.Children.Add(face);
            // 4
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[4], points[5], points[2], points[1], material);
            m3dg.Children.Add(face);
            // 5
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[5], points[3], points[0], points[2], material);
            m3dg.Children.Add(face);
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
