using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace UwpMasterDetailViewSample.Extensions
{
    public static class VisualTreeHelperExtensions
    {
        public static IEnumerable<DependencyObject> GetDescendants(this DependencyObject start, bool includeStart = false)
        {
            if (start == null)
            {
                yield break;
            }

            if (includeStart)
            {
                yield return start;
            }

            var queue = new Queue<DependencyObject>();

            var popup = start as Popup;

            if (popup != null)
            {
                if (popup.Child != null)
                {
                    queue.Enqueue(popup.Child);
                    yield return popup.Child;
                }
            }
            else
            {
                int childrenCount;

                try
                {
                    if (start is UIElement)
                    {
                        childrenCount = VisualTreeHelper.GetChildrenCount(start);
                    }
                    else
                    {
                        childrenCount = 0;
                    }
                }
                catch (Exception)
                {
                    childrenCount = 0;
                }

                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(start, i);
                    queue.Enqueue(child);
                    yield return child;
                }
            }

            while (queue.Count > 0)
            {
                var parent = queue.Dequeue();

                popup = parent as Popup;

                if (popup != null)
                {
                    if (popup.Child != null)
                    {
                        queue.Enqueue(popup.Child);
                        yield return popup.Child;
                    }
                }
                else
                {
                    int childrenCount;

                    try
                    {
                        if (start is DataTemplate)
                        {
                            childrenCount = 0;
                        }
                        else
                        {
                            childrenCount = VisualTreeHelper.GetChildrenCount(start);
                        }
                    }
                    catch (Exception)
                    {
                        childrenCount = 0;
                    }

                    for (int i = 0; i < childrenCount; i++)
                    {
                        var child = VisualTreeHelper.GetChild(parent, i);
                        yield return child;
                        queue.Enqueue(child);
                    }
                }
            }
        }

        public static IEnumerable<T> GetDescendantsOfType<T>(this DependencyObject start) where T : DependencyObject
        {
            return start.GetDescendants().OfType<T>();
        }
    }
}