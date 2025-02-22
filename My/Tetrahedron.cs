using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace lab1.My
{
    public class Tetrahedron : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public Tetrahedron()
        {
            Draw(_size, _pos, _front, _left, _right, _bottom);
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
                Draw(_size, _pos, _front, _left, _right, _bottom);
            }
        }
        //положение центра фигуры
        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;
                //после изменения значения поля фиксируем изменения
                Draw(_size, _pos, _front, _left, _right, _bottom);
            }
        }
        // Материалы граней цвета граней
        // Кисть для закраски передней грани
        private Brush _front = _defaultColor;
        //цвет передней грани
        public Brush Front
        {
            get => _front;
            set
            {
                _front = value;
                Draw(_size, _pos, _front, _left, _right, _bottom);
            }
        }
        private Brush _left = _defaultColor;
        public Brush Left
        {
            get => _left;
            set
            {
                _left = value;
                Draw(_size, _pos, _front, _left, _right, _bottom);
            }
        }
        private Brush _right = _defaultColor;
        public Brush Right
        {
            get => _right;
            set
            {
                _right = value;
                Draw(_size, _pos, _front, _left, _right, _bottom);
            }
        }
        private Brush _bottom = _defaultColor;
        public Brush Bottom
        {
            get => _bottom;
            set
            {
                _bottom = value;
                Draw(_size, _pos, _front, _left, _right, _bottom);
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
        private void Draw(
                        double size,
                        Point3D pos,
                        Brush front,
                        Brush left,
                        Brush right,
                        Brush bottom)
        {
            // Отсчёт точек от левого нижнего угла грани, против часовой стрелки.
            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;
            Point3D front_left_bottom = new Point3D(-absX * Math.Sin(Math.PI / 3) + pos.X, -absY * 0.5 + pos.Y, absZ * 0.75 + pos.Z);
            Point3D front_right_bottom = new Point3D(absX * Math.Sin(Math.PI / 3) + pos.X, -absY * 0.5 + pos.Y, absZ * 0.75 + pos.Z);
            Point3D top = new Point3D(pos.X, absY + pos.Y, pos.Z);
            Point3D backside_bottom = new Point3D(pos.X, -absY * 0.5 + pos.Y, -absZ * Math.Sin(Math.PI / 3) + pos.Z);
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            // 1 Передняя
            DiffuseMaterial material = new DiffuseMaterial(front);
            GeometryModel3D faceFront = AddFace(front_left_bottom, front_right_bottom, top, material);
            m3dg.Children.Add(faceFront);
            // 2 Левая
            material = new DiffuseMaterial(left);
            GeometryModel3D faceLeft = AddFace(backside_bottom, front_left_bottom, top, material);
            m3dg.Children.Add(faceLeft);
            // 3 Правая
            material = new DiffuseMaterial(right);
            GeometryModel3D faceRight = AddFace(front_right_bottom, backside_bottom, top, material);
            m3dg.Children.Add(faceRight);
            // 4 Нижняя
            material = new DiffuseMaterial(bottom);
            GeometryModel3D faceBottom = AddFace(backside_bottom, front_right_bottom, front_left_bottom, material);
            m3dg.Children.Add(faceBottom);
            //Сохранение данных объекта
            Content = m3dg;
        }
    }
}
