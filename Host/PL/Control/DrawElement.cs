using System.Windows;
using System.Windows.Media;

namespace Host.PL.Control
{
    public class DrawElement : FrameworkElement
    {
        private VisualCollection children;
        private DrawingVisual visual = new DrawingVisual();

        public DrawElement()
        {
            this.children = new VisualCollection(this) { this.visual };
        }

        public DrawingContext RenderOpen()
        {
            return this.visual.RenderOpen();
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.children[index];
        }
        protected override int VisualChildrenCount
        {
            get { return this.children.Count; }
        }
    }
}