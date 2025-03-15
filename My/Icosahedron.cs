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
    public class Icosahedron : ModelVisual3D
    {
        public Icosahedron() 
        {
            Draw(_size, _pos);
        }
        //размер по умолчанию
        private double _size = 0.5;
        //поле задания размера
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                //после изменения значения поля фиксируем изменения
                Draw(_size, _pos);
            }
        }
        //положение центра октаэдра
        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;
                //после изменения значения поля фиксируем изменения
                Draw(_size, _pos);
            }
        }

        //Добавление грани
        private static GeometryModel3D AddFace(
                        Point3D point1,
                        Point3D point2,
                        Point3D point3,  //вершины грани
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
        private void Draw(double size, Point3D pos)
        {
            // Отсчёт точек по 12 направлениям.
            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;
            double d = Math.Atan(2);
            Point3D[] points =
            {
                new Point3D(pos.X, pos.Y + absY, pos.Z),
                new Point3D(pos.X + absX * Math.Sin(d), pos.Y + absY * Math.Cos(d), pos.Z),
                new Point3D(pos.X + absX * Math.Sin(d) * Math.Cos(2 * Math.PI / 5), pos.Y + absY * Math.Cos(d), pos.Z - absZ * Math.Cos(d) * Math.Sin(2 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(d) * Math.Cos(4 * Math.PI / 5), pos.Y + absY * Math.Cos(d), pos.Z - absZ * Math.Cos(d) * Math.Sin(4 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(d) * Math.Cos(6 * Math.PI / 5), pos.Y + absY * Math.Cos(d), pos.Z - absZ * Math.Cos(d) * Math.Sin(6 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(d) * Math.Cos(8 * Math.PI / 5), pos.Y + absY * Math.Cos(d), pos.Z - absZ * Math.Cos(d) * Math.Sin(8 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(d) * Math.Cos(Math.PI / 5), pos.Y - absY * Math.Cos(d), pos.Z - absZ * Math.Cos(d) * Math.Sin(Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(d) * Math.Cos(3 * Math.PI / 5), pos.Y - absY * Math.Cos(d), pos.Z - absZ * Math.Cos(d) * Math.Sin(3 * Math.PI / 5)),
                new Point3D(pos.X - absX * Math.Sin(d), pos.Y - absY * Math.Cos(d), pos.Z),
                new Point3D(pos.X + absX * Math.Sin(d) * Math.Cos(3 * Math.PI / 5), pos.Y - absY * Math.Cos(d), pos.Z + absZ * Math.Cos(d) * Math.Sin(3 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(d) * Math.Cos(Math.PI / 5), pos.Y - absY * Math.Cos(d), pos.Z + absZ * Math.Cos(d) * Math.Sin(Math.PI / 5)),
                new Point3D(pos.X, pos.Y - absY, pos.Z)
            };
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            DiffuseMaterial material = new DiffuseMaterial(RandBrush());
            GeometryModel3D face = AddFace(points[0], points[2], points[1], material);
        }
        private Brush RandBrush()
        {
            Random rnd = new Random();
            return new SolidColorBrush(Color.FromRgb((byte)rnd.Next(), (byte)rnd.Next(), (byte)rnd.Next()));
        }
    }
}
