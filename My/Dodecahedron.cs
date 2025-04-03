using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace lab1.My
{
    public class Dodecahedron : ModelVisual3D
    {
        public Dodecahedron()
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
        //положение центра икосаэдра
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
                        Point3D point3,
                        Point3D point4,
                        Point3D point5,  //вершины грани
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

                        point1,
                        point3,
                        point4, //вершины второго треугольника

                        point1,
                        point4,
                        point5 //вершины третьего треугольника
                    }
                },
                Material = material //цвет грани
            };
            return geometryModel3D;
        }
        private void Draw(double size, Point3D pos)
        {
            // Отсчёт точек против часовой стрелки.
            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;
            double a = 4 * size / (1 + Math.Sqrt(5)) / Math.Sqrt(3);    //длина ребра
            double R = Math.Sqrt(10) * Math.Sqrt(5 + Math.Sqrt(5)) / 10 * a; //радиус вписанной окружности пятиугольника
            double alpha = Math.Asin(R / size);
            double beta = alpha + 2 * Math.Asin(a / size / 2);
            Point3D[] points =
            {
                new Point3D(pos.X + absX * Math.Sin(alpha), pos.Y + absY * Math.Cos(alpha), pos.Z),
                new Point3D(pos.X + absX * Math.Sin(alpha) * Math.Cos(2 * Math.PI / 5), pos.Y + absY * Math.Cos(alpha), pos.Z - absZ * Math.Sin(alpha) * Math.Sin(2 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(alpha) * Math.Cos(4 * Math.PI / 5), pos.Y + absY * Math.Cos(alpha), pos.Z - absZ * Math.Sin(alpha) * Math.Sin(4 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(alpha) * Math.Cos(4 * Math.PI / 5), pos.Y + absY * Math.Cos(alpha), pos.Z + absZ * Math.Sin(alpha) * Math.Sin(4 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(alpha) * Math.Cos(2 * Math.PI / 5), pos.Y + absY * Math.Cos(alpha), pos.Z + absZ * Math.Sin(alpha) * Math.Sin(2 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(beta), pos.Y + absY * Math.Cos(beta), pos.Z),
                new Point3D(pos.X + absX * Math.Sin(beta) * Math.Cos(2 * Math.PI / 5), pos.Y + absY * Math.Cos(beta), pos.Z - absZ * Math.Sin(beta) * Math.Sin(2 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(beta) * Math.Cos(4 * Math.PI / 5), pos.Y + absY * Math.Cos(beta), pos.Z - absZ * Math.Sin(beta) * Math.Sin(4 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(beta) * Math.Cos(4 * Math.PI / 5), pos.Y + absY * Math.Cos(beta), pos.Z + absZ * Math.Sin(beta) * Math.Sin(4 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(beta) * Math.Cos(2 * Math.PI / 5), pos.Y + absY * Math.Cos(beta), pos.Z + absZ * Math.Sin(beta) * Math.Sin(2 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(beta) * Math.Cos(Math.PI / 5), pos.Y - absY * Math.Cos(beta), pos.Z - absZ * Math.Sin(beta) * Math.Sin(Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(beta) * Math.Cos(3 * Math.PI / 5), pos.Y - absY * Math.Cos(beta), pos.Z - absZ * Math.Sin(beta) * Math.Sin(3 * Math.PI / 5)),
                new Point3D(pos.X - absX * Math.Sin(beta), pos.Y - absY * Math.Cos(beta), pos.Z),
                new Point3D(pos.X + absX * Math.Sin(beta) * Math.Cos(3 * Math.PI / 5), pos.Y - absY * Math.Cos(beta), pos.Z + absZ * Math.Sin(beta) * Math.Sin(3 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(beta) * Math.Cos(Math.PI / 5), pos.Y - absY * Math.Cos(beta), pos.Z + absZ * Math.Sin(beta) * Math.Sin(Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(alpha) * Math.Cos(Math.PI / 5), pos.Y - absY * Math.Cos(alpha), pos.Z - absZ * Math.Sin(alpha) * Math.Sin(Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(alpha) * Math.Cos(3 * Math.PI / 5), pos.Y - absY * Math.Cos(alpha), pos.Z - absZ * Math.Sin(alpha) * Math.Sin(3 * Math.PI / 5)),
                new Point3D(pos.X - absX * Math.Sin(alpha), pos.Y - absY * Math.Cos(alpha), pos.Z),
                new Point3D(pos.X + absX * Math.Sin(alpha) * Math.Cos(3 * Math.PI / 5), pos.Y - absY * Math.Cos(alpha), pos.Z + absZ * Math.Sin(alpha) * Math.Sin(3 * Math.PI / 5)),
                new Point3D(pos.X + absX * Math.Sin(alpha) * Math.Cos(Math.PI / 5), pos.Y - absY * Math.Cos(alpha), pos.Z + absZ * Math.Sin(alpha) * Math.Sin(Math.PI / 5))
            };
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            // Верхняя часть
            // 1
            DiffuseMaterial material = new DiffuseMaterial(RandBrush());
            GeometryModel3D face = AddFace(points[0], points[1], points[2], points[3], points[4], material);
            m3dg.Children.Add(face);
            // 2
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[0], points[5], points[10], points[6], points[1], material);
            m3dg.Children.Add(face);
            // 3
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[1], points[6], points[11], points[7], points[2], material);
            m3dg.Children.Add(face);
            // 4
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[2], points[7], points[12], points[8], points[3], material);
            m3dg.Children.Add(face);
            // 5
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[3], points[8], points[13], points[9], points[4], material);
            m3dg.Children.Add(face);
            // 6
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[4], points[9], points[14], points[5], points[0], material);
            m3dg.Children.Add(face);
            // Нижняя часть
            // 7
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[5], points[14], points[19], points[15], points[10], material);
            m3dg.Children.Add(face);
            // 8
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[6], points[10], points[15], points[16], points[11], material);
            m3dg.Children.Add(face);
            // 9
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[7], points[11], points[16], points[17], points[12], material);
            m3dg.Children.Add(face);
            // 10
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[8], points[12], points[17], points[18], points[13], material);
            m3dg.Children.Add(face);
            // 11
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[9], points[13], points[18], points[19], points[14], material);
            m3dg.Children.Add(face);
            // 12
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points[19], points[18], points[17], points[16], points[15], material);
            m3dg.Children.Add(face);
            //Сохранение данных объекта
            Content = m3dg;
        }
        private Random rnd = new Random();
        private Brush RandBrush()
        {
            return new SolidColorBrush(Color.FromRgb((byte)rnd.Next(), (byte)rnd.Next(), (byte)rnd.Next()));
        }
    }
}
