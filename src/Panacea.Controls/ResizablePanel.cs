using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Panacea.Controls
{
    public class ResizablePanel : WrapPanel
    {
        public double TileWidth { get; set; }


        public int ItemsCount
        {
            get { return (int)GetValue(ItemsCountProperty); }
            set { SetValue(ItemsCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsCountProperty =
            DependencyProperty.Register("ItemsCount", typeof(int), typeof(ResizablePanel), new PropertyMetadata(0));


        protected override Size MeasureOverride(Size constraint)
        {
            //return base.MeasureOverride(constraint);
            int minTileWidth = 150;
            int maxColumns = 5;
            int columns;

            double width = constraint.Width;
            double height = constraint.Height;

            if (width <= minTileWidth)
            {
                TileWidth = width;
                columns = 1;
            }
            else
            {
                columns = (int) width / minTileWidth;
            }

            if (columns > maxColumns)
            {
                columns = maxColumns;
                TileWidth = width / maxColumns;
            }

            int rows = (int) height / (int) TileWidth / 2;
            ItemsCount = columns * rows;
            return new Size(width, height);
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }
       
    }
}
