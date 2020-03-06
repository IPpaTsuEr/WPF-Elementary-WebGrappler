using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WebGrappler.Helpers
{
    class LoadingManager
    {
        private static void LoadingStatusChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var _target = dp as UIElement;
            if (_target == null) return;

            var _loading = (bool)e.NewValue;

            if ((_target.RenderTransform as TransformGroup) == null)
            {
                var OT = _target.RenderTransform;
                _target.RenderTransform = new TransformGroup() { Children = new TransformCollection() { OT, new RotateTransform() } };
            }
            else 
            {
                var RTG = _target.RenderTransform as TransformGroup;
                RTG.Children.Add(new RotateTransform());
            }
            var T = (_target.RenderTransform as TransformGroup).Children.Last();

            if (_loading)
            {
                T.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimationUsingKeyFrames()
                {
                    RepeatBehavior = RepeatBehavior.Forever,
                    Duration = new Duration(TimeSpan.FromSeconds(4)),
                    KeyFrames = new DoubleKeyFrameCollection() {
                    new EasingDoubleKeyFrame(0,KeyTime.FromPercent(0)),
                    new EasingDoubleKeyFrame(19,KeyTime.FromPercent(0.05),new CubicEase(){ EasingMode=EasingMode.EaseIn }),
                    new EasingDoubleKeyFrame(341,KeyTime.FromPercent(0.95),new CubicEase(){ EasingMode=EasingMode.EaseOut }),
                    new EasingDoubleKeyFrame(360,KeyTime.FromPercent(1))
                 }
                });
            }
            else
            {
                T.BeginAnimation(RotateTransform.AngleProperty, null);
                (_target.RenderTransform as TransformGroup).Children.Remove(T);
            }
        }

        public static bool GetIsLoading(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLoadingProperty);
        }

        public static void SetIsLoading(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLoadingProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.RegisterAttached("IsLoading", typeof(bool), typeof(LoadingManager), new PropertyMetadata(true,new PropertyChangedCallback(LoadingStatusChanged)));


    }
}
