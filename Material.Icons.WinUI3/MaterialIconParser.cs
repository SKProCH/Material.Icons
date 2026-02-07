using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace Material.Icons.WinUI3;

/// <summary>
/// High-performance SVG Path parser using unsafe code for maximum speed.
/// Converts SVG path data strings directly to WinUI3 PathGeometry objects.
/// Optimized specifically for Material Icons (mostly LineSegment commands).
/// </summary>
public static unsafe class MaterialIconParser {
    /// <summary>
    /// Parses SVG path data string into a PathGeometry object.
    /// </summary>
    /// <param name="source">SVG path data string (e.g., "M10 20L30 40")</param>
    /// <returns>Parsed PathGeometry ready for rendering</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PathGeometry Parse(string source) {
        if (string.IsNullOrEmpty(source))
            return new PathGeometry();

        var geometry = new PathGeometry();
        var figures = new PathFigureCollection();
        geometry.Figures = figures;

        fixed (char* ptr = source) {
            ParseCore(ptr, source.Length, figures);
        }

        return geometry;
    }

    /// <summary>
    /// Core parsing loop with zero-allocation design.
    /// Uses pointer arithmetic for maximum performance.
    /// </summary>
    private static void ParseCore(char* source, int length, PathFigureCollection figures) {
        // Current pen position and subpath start position
        double currentX = 0, currentY = 0;
        double startX = 0, startY = 0;

        // Current command state
        char currentCommand = '\0';
        bool isRelative = false;
        PathFigure? currentFigure = null;

        int i = 0;

        while (i < length) {
            // Skip whitespace and commas efficiently
            char c;
            while (i < length && ((c = source[i]) == ' ' || c == '\t' || c == '\r' || c == '\n' || c == ','))
                i++;

            if (i >= length)
                break;

            char ch = source[i];

            // Check for command letters (A-Z, a-z)
            if ((uint)(ch - 'A') <= ('Z' - 'A') || (uint)(ch - 'a') <= ('z' - 'a')) {
                currentCommand = ch;
                isRelative = ch >= 'a';
                i++;

                // Handle close path command (Z)
                if ((ch & 0xDF) == 'Z') // Case-insensitive comparison using bitwise AND
                {
                    if (currentFigure is not null) {
                        currentFigure.IsClosed = true;
                        figures.Add(currentFigure);
                        currentX = startX;
                        currentY = startY;
                        currentFigure = null;
                    }
                    continue;
                }

                // Skip whitespace after command
                while (i < length && ((c = source[i]) == ' ' || c == '\t' || c == '\r' || c == '\n' || c == ','))
                    i++;
            }
            else if (currentCommand == '\0') {
                i++;
                continue;
            }

            // Check if next character starts a number
            if (i >= length || !IsNumberStart(source[i]))
                continue;

            // Process current command (convert to uppercase for switch)
            switch (currentCommand & 0xDF) // Bitwise AND converts to uppercase
            {
                case 'M': // Move to (start new subpath)
                    i = ProcessMove(source, length, i, isRelative,
                                  ref currentX, ref currentY,
                                  ref startX, ref startY,
                                  ref currentFigure, figures);
                    currentCommand = isRelative ? 'l' : 'L'; // Implicit line-to after move
                    break;

                case 'L': // Line to
                    i = ProcessLine(source, length, i, isRelative,
                                  ref currentX, ref currentY,
                                  ref currentFigure);
                    break;

                case 'H': // Horizontal line to
                    i = ProcessHorizontal(source, length, i, isRelative,
                                        ref currentX, ref currentY,
                                        ref currentFigure);
                    break;

                case 'V': // Vertical line to
                    i = ProcessVertical(source, length, i, isRelative,
                                      ref currentX, ref currentY,
                                      ref currentFigure);
                    break;

                case 'C': // Cubic Bézier curve
                    i = ProcessCubicBezier(source, length, i, isRelative,
                                         ref currentX, ref currentY,
                                         ref currentFigure);
                    break;

                case 'S': // Smooth cubic Bézier curve
                    i = ProcessSmoothCubic(source, length, i, isRelative,
                                         ref currentX, ref currentY,
                                         ref currentFigure);
                    break;

                case 'Q': // Quadratic Bézier curve
                    i = ProcessQuadraticBezier(source, length, i, isRelative,
                                             ref currentX, ref currentY,
                                             ref currentFigure);
                    break;

                case 'T': // Smooth quadratic Bézier curve
                    i = ProcessSmoothQuadratic(source, length, i, isRelative,
                                             ref currentX, ref currentY,
                                             ref currentFigure);
                    break;

                case 'A': // Elliptical arc
                    i = ProcessArc(source, length, i, isRelative,
                                 ref currentX, ref currentY,
                                 ref currentFigure);
                    break;
            }
        }

        // Add the last figure if it exists
        if (currentFigure is not null)
            figures.Add(currentFigure);
    }

    /// <summary>
    /// Processes 'M' (move to) command.
    /// Starts a new subpath and sets the current point.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ProcessMove(char* source, int length, int index, bool isRelative,
                                 ref double x, ref double y,
                                 ref double startX, ref double startY,
                                 ref PathFigure? currentFigure, PathFigureCollection figures) {
        // Close previous figure if exists
        if (currentFigure is not null)
            figures.Add(currentFigure);

        // Read new coordinates
        index = ReadNumber(source, length, index, out double newX);
        index = ReadNumber(source, length, index, out double newY);

        if (isRelative) {
            newX += x;
            newY += y;
        }

        // Create new figure starting at the new point
        currentFigure = new PathFigure {
            StartPoint = new Point(newX, newY)
        };

        x = newX;
        y = newY;
        startX = x;
        startY = y;

        // Process implicit line-to commands (coordinates after move)
        while (index < length && IsNumberStart(source[index])) {
            index = ReadNumber(source, length, index, out double nextX);
            index = ReadNumber(source, length, index, out double nextY);

            if (isRelative) {
                nextX += x;
                nextY += y;
            }

            currentFigure.Segments.Add(new LineSegment { Point = new Point(nextX, nextY) });
            x = nextX;
            y = nextY;
        }

        return index;
    }

    /// <summary>
    /// Processes 'L' (line to) command.
    /// Draws straight lines from current point to new points.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ProcessLine(char* source, int length, int index, bool isRelative,
                                 ref double x, ref double y, ref PathFigure? currentFigure) {
        // Create figure if none exists (non-standard but handled for robustness)
        currentFigure ??= new PathFigure { StartPoint = new Point(x, y) };

        index = ReadNumber(source, length, index, out double newX);
        index = ReadNumber(source, length, index, out double newY);

        if (isRelative) {
            newX += x;
            newY += y;
        }

        currentFigure.Segments.Add(new LineSegment { Point = new Point(newX, newY) });
        x = newX;
        y = newY;

        // Process multiple coordinate pairs (implicit line continuation)
        while (index < length && IsNumberStart(source[index])) {
            index = ReadNumber(source, length, index, out double nextX);
            index = ReadNumber(source, length, index, out double nextY);

            if (isRelative) {
                nextX += x;
                nextY += y;
            }

            currentFigure.Segments.Add(new LineSegment { Point = new Point(nextX, nextY) });
            x = nextX;
            y = nextY;
        }

        return index;
    }

    /// <summary>
    /// Reads a double number from the character buffer.
    /// Handles signs, integer parts, and decimal fractions.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ReadNumber(char* source, int length, int index, out double result) {
        // Skip whitespace and commas
        while (index < length) {
            char c = source[index];
            if (c != ' ' && c != '\t' && c != '\r' && c != '\n' && c != ',')
                break;
            index++;
        }

        if (index >= length) {
            result = 0;
            return index;
        }

        bool negative = false;

        // Handle sign
        if (source[index] == '-') {
            negative = true;
            index++;
        }
        else if (source[index] == '+') {
            index++;
        }

        // Parse integer part
        double value = 0;
        bool hasDigits = false;

        while (index < length && (uint)(source[index] - '0') <= 9) {
            value = value * 10 + (source[index] - '0');
            hasDigits = true;
            index++;
        }

        // Parse fractional part
        if (index < length && source[index] == '.') {
            index++;
            double fraction = 0.1;
            while (index < length && (uint)(source[index] - '0') <= 9) {
                value += (source[index] - '0') * fraction;
                fraction *= 0.1;
                hasDigits = true;
                index++;
            }
        }

        result = hasDigits ? (negative ? -value : value) : 0;
        return index;
    }

    /// <summary>
    /// Checks if a character can start a number.
    /// Valid starters: '-', '+', '.', or digits 0-9.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsNumberStart(char c) {
        return c == '-' || c == '+' || c == '.' || ((uint)(c - '0') <= 9);
    }

    /// <summary>
    /// Processes 'H' (horizontal line to) command.
    /// Draws horizontal lines from current X to new X positions.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ProcessHorizontal(char* source, int length, int index, bool isRelative,
                                       ref double x, ref double y, ref PathFigure? currentFigure) {
        currentFigure ??= new PathFigure { StartPoint = new Point(x, y) };

        index = ReadNumber(source, length, index, out double newX);

        if (isRelative)
            newX += x;

        currentFigure.Segments.Add(new LineSegment { Point = new Point(newX, y) });
        x = newX;

        while (index < length && IsNumberStart(source[index])) {
            index = ReadNumber(source, length, index, out double nextX);

            if (isRelative)
                nextX += x;

            currentFigure.Segments.Add(new LineSegment { Point = new Point(nextX, y) });
            x = nextX;
        }

        return index;
    }

    /// <summary>
    /// Processes 'V' (vertical line to) command.
    /// Draws vertical lines from current Y to new Y positions.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ProcessVertical(char* source, int length, int index, bool isRelative,
                                     ref double x, ref double y, ref PathFigure? currentFigure) {
        currentFigure ??= new PathFigure { StartPoint = new Point(x, y) };

        index = ReadNumber(source, length, index, out double newY);

        if (isRelative)
            newY += y;

        currentFigure.Segments.Add(new LineSegment { Point = new Point(x, newY) });
        y = newY;

        while (index < length && IsNumberStart(source[index])) {
            index = ReadNumber(source, length, index, out double nextY);

            if (isRelative)
                nextY += y;

            currentFigure.Segments.Add(new LineSegment { Point = new Point(x, nextY) });
            y = nextY;
        }

        return index;
    }

    /// <summary>
    /// Processes 'C' (cubic Bézier curve) command.
    /// Draws cubic Bézier curves with two control points.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ProcessCubicBezier(char* source, int length, int index, bool isRelative,
                                        ref double x, ref double y, ref PathFigure? currentFigure) {
        currentFigure ??= new PathFigure { StartPoint = new Point(x, y) };

        // Read 6 values: control1 X/Y, control2 X/Y, end X/Y
        index = ReadNumber(source, length, index, out double x1);
        index = ReadNumber(source, length, index, out double y1);
        index = ReadNumber(source, length, index, out double x2);
        index = ReadNumber(source, length, index, out double y2);
        index = ReadNumber(source, length, index, out double newX);
        index = ReadNumber(source, length, index, out double newY);

        if (isRelative) {
            x1 += x;
            y1 += y;
            x2 += x;
            y2 += y;
            newX += x;
            newY += y;
        }

        currentFigure.Segments.Add(new BezierSegment {
            Point1 = new Point(x1, y1),
            Point2 = new Point(x2, y2),
            Point3 = new Point(newX, newY)
        });

        x = newX;
        y = newY;

        while (index < length && IsNumberStart(source[index])) {
            index = ReadNumber(source, length, index, out double x1b);
            index = ReadNumber(source, length, index, out double y1b);
            index = ReadNumber(source, length, index, out double x2b);
            index = ReadNumber(source, length, index, out double y2b);
            index = ReadNumber(source, length, index, out double nextX);
            index = ReadNumber(source, length, index, out double nextY);

            if (isRelative) {
                x1b += x;
                y1b += y;
                x2b += x;
                y2b += y;
                nextX += x;
                nextY += y;
            }

            currentFigure.Segments.Add(new BezierSegment {
                Point1 = new Point(x1b, y1b),
                Point2 = new Point(x2b, y2b),
                Point3 = new Point(nextX, nextY)
            });

            x = nextX;
            y = nextY;
        }

        return index;
    }

    /// <summary>
    /// Processes 'S' (smooth cubic Bézier curve) command.
    /// Draws cubic Bézier curves where first control point is reflection of previous second control point.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ProcessSmoothCubic(char* source, int length, int index, bool isRelative,
                                        ref double x, ref double y, ref PathFigure? currentFigure) {
        currentFigure ??= new PathFigure { StartPoint = new Point(x, y) };

        // Read 4 values: control2 X/Y, end X/Y
        index = ReadNumber(source, length, index, out double x2);
        index = ReadNumber(source, length, index, out double y2);
        index = ReadNumber(source, length, index, out double newX);
        index = ReadNumber(source, length, index, out double newY);

        if (isRelative) {
            x2 += x;
            y2 += y;
            newX += x;
            newY += y;
        }

        // First control point is reflection of previous second control point
        currentFigure.Segments.Add(new BezierSegment {
            Point1 = new Point(x, y),
            Point2 = new Point(x2, y2),
            Point3 = new Point(newX, newY)
        });

        x = newX;
        y = newY;

        while (index < length && IsNumberStart(source[index])) {
            index = ReadNumber(source, length, index, out double x2b);
            index = ReadNumber(source, length, index, out double y2b);
            index = ReadNumber(source, length, index, out double nextX);
            index = ReadNumber(source, length, index, out double nextY);

            if (isRelative) {
                x2b += x;
                y2b += y;
                nextX += x;
                nextY += y;
            }

            currentFigure.Segments.Add(new BezierSegment {
                Point1 = new Point(x, y),
                Point2 = new Point(x2b, y2b),
                Point3 = new Point(nextX, nextY)
            });

            x = nextX;
            y = nextY;
        }

        return index;
    }

    /// <summary>
    /// Processes 'Q' (quadratic Bézier curve) command.
    /// Draws quadratic Bézier curves with one control point.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ProcessQuadraticBezier(char* source, int length, int index, bool isRelative,
                                            ref double x, ref double y, ref PathFigure? currentFigure) {
        currentFigure ??= new PathFigure { StartPoint = new Point(x, y) };

        // Read 4 values: control X/Y, end X/Y
        index = ReadNumber(source, length, index, out double x1);
        index = ReadNumber(source, length, index, out double y1);
        index = ReadNumber(source, length, index, out double newX);
        index = ReadNumber(source, length, index, out double newY);

        if (isRelative) {
            x1 += x;
            y1 += y;
            newX += x;
            newY += y;
        }

        currentFigure.Segments.Add(new QuadraticBezierSegment {
            Point1 = new Point(x1, y1),
            Point2 = new Point(newX, newY)
        });

        x = newX;
        y = newY;

        while (index < length && IsNumberStart(source[index])) {
            index = ReadNumber(source, length, index, out double x1b);
            index = ReadNumber(source, length, index, out double y1b);
            index = ReadNumber(source, length, index, out double nextX);
            index = ReadNumber(source, length, index, out double nextY);

            if (isRelative) {
                x1b += x;
                y1b += y;
                nextX += x;
                nextY += y;
            }

            currentFigure.Segments.Add(new QuadraticBezierSegment {
                Point1 = new Point(x1b, y1b),
                Point2 = new Point(nextX, nextY)
            });

            x = nextX;
            y = nextY;
        }

        return index;
    }

    /// <summary>
    /// Processes 'T' (smooth quadratic Bézier curve) command.
    /// Draws quadratic Bézier curves where control point is reflection of previous control point.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ProcessSmoothQuadratic(char* source, int length, int index, bool isRelative,
                                            ref double x, ref double y, ref PathFigure? currentFigure) {
        currentFigure ??= new PathFigure { StartPoint = new Point(x, y) };

        // Read 2 values: end X/Y
        index = ReadNumber(source, length, index, out double newX);
        index = ReadNumber(source, length, index, out double newY);

        if (isRelative) {
            newX += x;
            newY += y;
        }

        // Control point is reflection of previous control point
        currentFigure.Segments.Add(new QuadraticBezierSegment {
            Point1 = new Point(x, y),
            Point2 = new Point(newX, newY)
        });

        x = newX;
        y = newY;

        while (index < length && IsNumberStart(source[index])) {
            index = ReadNumber(source, length, index, out double nextX);
            index = ReadNumber(source, length, index, out double nextY);

            if (isRelative) {
                nextX += x;
                nextY += y;
            }

            currentFigure.Segments.Add(new QuadraticBezierSegment {
                Point1 = new Point(x, y),
                Point2 = new Point(nextX, nextY)
            });

            x = nextX;
            y = nextY;
        }

        return index;
    }

    /// <summary>
    /// Processes 'A' (elliptical arc) command.
    /// Draws elliptical arcs with complex parameters.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ProcessArc(char* source, int length, int index, bool isRelative,
                                ref double x, ref double y, ref PathFigure? currentFigure) {
        currentFigure ??= new PathFigure { StartPoint = new Point(x, y) };

        // Read 7 values: rx, ry, rotation, large-arc-flag, sweep-flag, end X/Y
        index = ReadNumber(source, length, index, out double rx);
        index = ReadNumber(source, length, index, out double ry);
        index = ReadNumber(source, length, index, out double rotation);
        index = ReadNumber(source, length, index, out double largeArcFlag);
        index = ReadNumber(source, length, index, out double sweepFlag);
        index = ReadNumber(source, length, index, out double newX);
        index = ReadNumber(source, length, index, out double newY);

        if (isRelative) {
            newX += x;
            newY += y;
        }

        // Convert flags to boolean/enum (flags are 0 or 1 in SVG)
        bool isLargeArc = largeArcFlag > 0.5;
        var sweepDirection = sweepFlag > 0.5
            ? SweepDirection.Clockwise
            : SweepDirection.Counterclockwise;

        currentFigure.Segments.Add(new ArcSegment {
            Size = new Size(rx, ry),
            RotationAngle = rotation,
            IsLargeArc = isLargeArc,
            SweepDirection = sweepDirection,
            Point = new Point(newX, newY)
        });

        x = newX;
        y = newY;

        while (index < length && IsNumberStart(source[index])) {
            index = ReadNumber(source, length, index, out double rxb);
            index = ReadNumber(source, length, index, out double ryb);
            index = ReadNumber(source, length, index, out double rotationb);
            index = ReadNumber(source, length, index, out double largeArcFlagb);
            index = ReadNumber(source, length, index, out double sweepFlagb);
            index = ReadNumber(source, length, index, out double nextX);
            index = ReadNumber(source, length, index, out double nextY);

            if (isRelative) {
                nextX += x;
                nextY += y;
            }

            bool isLargeArcb = largeArcFlagb > 0.5;
            var sweepDirectionb = sweepFlagb > 0.5
                ? SweepDirection.Clockwise
                : SweepDirection.Counterclockwise;

            currentFigure.Segments.Add(new ArcSegment {
                Size = new Size(rxb, ryb),
                RotationAngle = rotationb,
                IsLargeArc = isLargeArcb,
                SweepDirection = sweepDirectionb,
                Point = new Point(nextX, nextY)
            });

            x = nextX;
            y = nextY;
        }

        return index;
    }
}