using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Panacea.Controls
{
    public class ResizablePanel : UniformGrid
    {

        public ResizablePanel()
        {

        }

        public int Capacity
        {
            get { return (int)GetValue(CapacityProperty); }
            set { SetValue(CapacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Capacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CapacityProperty =
            DependencyProperty.Register("Capacity", typeof(int), typeof(ResizablePanel), new PropertyMetadata(0));


        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            double width = ActualWidth;
            double height = ActualHeight;

            if (width>0 && height>0)
            {
                if (width >= height)
                {
                    //landscape
                    
                    Rows = 2;
                }
                else
                {
                    //portrait
                    Rows = 4;
                }

                double itemHeight = height / Rows;
                double itemWidth = itemHeight / 2;

                Columns = (int)width / (int)itemWidth;

                Capacity = Columns * Rows;
            }

        }


    }
}
