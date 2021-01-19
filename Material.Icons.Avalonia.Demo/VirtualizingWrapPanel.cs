// using System;
// using System.Collections.Generic;
// using System.Collections.Specialized;
// using Avalonia;
// using Avalonia.Collections;
// using Avalonia.Controls;
// using Avalonia.Controls.Templates;
// using Avalonia.Data;
// using Avalonia.Input;
// using Avalonia.Interactivity;
// using Avalonia.Layout;
// using Avalonia.LogicalTree;
// using Avalonia.Media;
// using Avalonia.Rendering;
// using Avalonia.Styling;
// using Avalonia.VisualTree;
//
// namespace Material.Icons.Avalonia.Demo {
//     public class VirtualizingWrapPanel : Panel, IVirtualizingPanel {
//         private bool _forceRemeasure;
//         private double _crossAxisOffset;
//         private double _pixelOffset;
//         private Size _availableSpace;
//         private int _canBeRemoved;
//
//         public void ForceInvalidateMeasure() {
//             InvalidateMeasure();
//             _forceRemeasure = true;
//         }
//
//         public IVirtualizingController Controller { get; set; }
//         public bool IsFull { get; }
//         public int OverflowCount { get; }
//         public Orientation ScrollDirection { get; }
//         public double AverageItemSize { get; }
//         public double PixelOverflow { get; }
//         
//         public double PixelOffset
//         {
//             get => _pixelOffset;
//
//             set {
//                 // ReSharper disable once CompareOfFloatsByEqualityOperator
//                 if (_pixelOffset == value) return;
//                 _pixelOffset = value;
//                 InvalidateArrange();
//             }
//         }
//
//         public double CrossAxisOffset {
//             get => _crossAxisOffset;
//
//             set {
//                 // ReSharper disable once CompareOfFloatsByEqualityOperator
//                 if (_crossAxisOffset == value) return;
//                 _crossAxisOffset = value;
//                 InvalidateArrange();
//             }
//         }
//         
//         protected override Size MeasureOverride(Size availableSize)
//         {
//             if (_forceRemeasure || availableSize != ((ILayoutable)this).PreviousMeasure)
//             {
//                 _forceRemeasure = false;
//                 _availableSpace = availableSize;
//                 Controller?.UpdateControls();
//             }
//
//             return base.MeasureOverride(availableSize);
//         }
//         
//         protected override Size ArrangeOverride(Size finalSize)
//         {
//             _availableSpace = finalSize;
//             _canBeRemoved = 0;
//             _takenSpace = 0;
//             _averageItemSize = 0;
//             _averageCount = 0;
//             var result = base.ArrangeOverride(finalSize);
//             _takenSpace += _pixelOffset;
//             Controller?.UpdateControls();
//             return result;
//         }
//
//         protected override void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
//         {
//             base.ChildrenChanged(sender, e);
//
//             switch (e.Action)
//             {
//                 case NotifyCollectionChangedAction.Add:
//                     foreach (IControl control in e.NewItems)
//                     {
//                         UpdateAdd(control);
//                     }
//
//                     break;
//
//                 case NotifyCollectionChangedAction.Remove:
//                     foreach (IControl control in e.OldItems)
//                     {
//                         UpdateRemove(control);
//                     }
//
//                     break;
//             }
//         }
//         
//         private void UpdateAdd(IControl child)
//         {
//             var bounds = Bounds;
//             var spacing = Spacing;
//
//             child.Measure(_availableSpace);
//             ++_averageCount;
//
//             if (Orientation == Orientation.Vertical)
//             {
//                 var height = child.DesiredSize.Height;
//                 _takenSpace += height + spacing;
//                 AddToAverageItemSize(height);
//             }
//             else
//             {
//                 var width = child.DesiredSize.Width;
//                 _takenSpace += width + spacing;
//                 AddToAverageItemSize(width);
//             }
//         }
//
//         private void UpdateRemove(IControl child)
//         {
//             var bounds = Bounds;
//
//             if (Orientation == Orientation.Vertical)
//             {
//                 var height = child.DesiredSize.Height;
//                 _takenSpace -= height + spacing;
//                 RemoveFromAverageItemSize(height);
//             }
//             else
//             {
//                 var width = child.DesiredSize.Width;
//                 _takenSpace -= width + spacing;
//                 RemoveFromAverageItemSize(width);
//             }
//
//             if (_canBeRemoved > 0)
//             {
//                 --_canBeRemoved;
//             }
//         }
//     }
// }