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
    public class Parallelepiped : ModelVisual3D
    {
        public Parallelepiped()
        {
            Draw(_length, _width, _height);
        }
        private double _length;
        public double Length
        {
            get => _length;
            set
            {
                _length = value;
                Draw(_length, _width, _height);
            }
        }
        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                Draw(_length, _width, _height);
            }
        }
        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                Draw(_length, _width, _height);
            }
        }
        private Brush _color;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                Draw(_length, _width, _height);
            }
        }

        //Добавление грани
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
        private void Draw(double length, double width, double height)
        {
            length /= 2;
            width /= 2;
            height /= 2;
            Point3D[] points =
            {
                new Point3D(-length, -height, -width),
                new Point3D(-length, height, -width),
                new Point3D(length, height, -width),
                new Point3D(length, -height, -width),
                new Point3D(-length, -height, width),
                new Point3D(-length, height, width),
                new Point3D(length, height, width),
                new Point3D(length, -height, width)
            };
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            // 1
            DiffuseMaterial material = new DiffuseMaterial(RandBrush());
            GeometryModel3D face = AddFace(points[0], points[1], points[2], points[3], material);
            m3dg.Children.Add(face);
            // 2
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[7], points[6], points[5], points[4], material);
            m3dg.Children.Add(face);
            // 3
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[0], points[3], points[7], points[4], material);
            m3dg.Children.Add(face);
            // 4
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[2], points[1], points[5], points[6], material);
            m3dg.Children.Add(face);
            // 5
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[0], points[4], points[5], points[1], material);
            m3dg.Children.Add(face);
            // 6
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[3], points[2], points[6], points[7], material);
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
