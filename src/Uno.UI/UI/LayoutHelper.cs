﻿using System;
using System.Collections.Generic;
using System.Text;
using Windows.Foundation;
using Windows.UI.Xaml;
using static System.Double;
using static System.Math;

namespace Uno.UI
{
	internal static class LayoutHelper
	{
		internal static (Size min, Size max) GetMinMax(this FrameworkElement e)
		{
			var size = new Size(e.Width, e.Height);
			var minSize = new Size(e.MinWidth, e.MinHeight);
			var maxSize = new Size(e.MaxWidth, e.MaxHeight);

			minSize = size
				.NumberOrDefault(new Size(0, 0))
				.AtMost(maxSize)
				.AtLeast(minSize);

			maxSize = size
				.NumberOrDefault(new Size(PositiveInfinity, PositiveInfinity))
				.AtMost(maxSize)
				.AtLeast(minSize);

			return (minSize, maxSize);
		}

		internal static Size GetMarginSize(this FrameworkElement frameworkElement)
		{
			var margin = frameworkElement.Margin;
			var marginWidth = margin.Left + margin.Right;
			var marginHeight = margin.Top + margin.Bottom;
			return new Size(marginWidth, marginHeight);
		}

		internal static Point GetAlignmentOffset(this FrameworkElement e, Size clientSize, Size renderSize)
		{
			// Start with Bottom-Right alignment, multiply by 0/0.5/1 for Top-Left/Center/Bottom-Right alignment
			var offset = new Point(
				clientSize.Width - renderSize.Width,
				clientSize.Height - renderSize.Height
			);

			switch (e.HorizontalAlignment)
			{
				case HorizontalAlignment.Stretch when renderSize.Width > clientSize.Width:
				case HorizontalAlignment.Left:
					offset.X *= 0;
					break;
				case HorizontalAlignment.Stretch:
				case HorizontalAlignment.Center:
					offset.X *= 0.5;
					break;
				case HorizontalAlignment.Right:
					offset.X *= 1;
					break;
			}

			switch (e.VerticalAlignment)
			{
				case VerticalAlignment.Stretch when renderSize.Height > clientSize.Height:
				case VerticalAlignment.Top:
					offset.Y *= 0;
					break;
				case VerticalAlignment.Stretch:
				case VerticalAlignment.Center:
					offset.Y *= 0.5;
					break;
				case VerticalAlignment.Bottom:
					offset.Y *= 1;
					break;
			}

			return offset;
		}

		internal static Size Min(Size val1, Size val2)
		{
			return new Size(
				Math.Min(val1.Width, val2.Width),
				Math.Min(val1.Height, val2.Height)
			);
		}

		internal static Size Max(Size val1, Size val2)
		{
			return new Size(
				Math.Max(val1.Width, val2.Width),
				Math.Max(val1.Height, val2.Height)
			);
		}

		internal static Size Add(this Size left, Size right)
		{
			return new Size(
				left.Width + right.Width,
				left.Height + right.Height
			);
		}

		internal static Size Subtract(this Size left, Size right)
		{
			return new Size(
				left.Width - right.Width,
				left.Height - right.Height
			);
		}

		public static double NumberOrDefault(this double value, double defaultValue)
		{
			return IsNaN(value)
				? defaultValue
				: value;
		}

		public static Size NumberOrDefault(this Size value, Size defaultValue)
		{
			return new Size(
				value.Width.NumberOrDefault(defaultValue.Width),
				value.Height.NumberOrDefault(defaultValue.Height)
			);
		}

		public static double AtMost(this double value, double most)
		{
			return Math.Min(value, most);
		}

		public static Size AtMost(this Size value, Size most)
		{
			return new Size(
				value.Width.AtMost(most.Width),
				value.Height.AtMost(most.Height)
			);
		}

		public static double AtLeast(this double value, double least)
		{
			return Math.Max(value, least);
		}

		public static Size AtLeast(this Size value, Size least)
		{
			return new Size(
				value.Width.AtLeast(least.Width),
				value.Height.AtLeast(least.Height)
			);
		}
	}
}