using System;
using System.Windows;
using Windows.Foundation;
namespace _2048_3d_game.Controller
{
    /// <summary>
    /// Class interprets what kind of gesture was made by player
    /// </summary>
    class GestureInterpreter
    {
        //Gesture direction
        private enum GestureDirection : int { Up = 0, Right = 1, Down = 2, Left = 3, Bottom = 4, Top = 5, None = -1 };

        //Number of fingers used in gesture
        private uint numberOfPointersPressed = 0;
        //Start point of gesture
        private Windows.Foundation.Point startPoint;
        //End point of gesture
        private Windows.Foundation.Point endPoint;

        //Distance between start and end points
        private double distanceBetweenPoints;
        //Angle between (start and end points) vector and (start + [50, 0]) vector
        private double angleBetweenVectors;
        private GestureDirection gestureDirection;

        //Minimal distance between start and end points to be recognized as proper gesture
        private double minimumDistanceOfMotionRecognition = 50;

        /// <summary>
        /// Constructor
        /// </summary>
        public GestureInterpreter()
        {

        }

        /// <summary>
        /// Method increases value of <see cref="numberOfPointersPressed">numberOfPointersPressed</see>.
        /// </summary>
        public void PointerPressed()
        {
            numberOfPointersPressed += 1;
        }

        /// <summary>
        /// Method clears value of
        /// *<see cref="numberOfPointersPressed">numberOfPointersPressed</see>
        /// *<see cref="distanceBetweenPoints">distanceBetweenPoints</see>
        /// *<see cref="angleBetweenVectors">angleBetweenVectors</see>
        /// *<see cref="gestureDirection">gestureDirection</see>
        /// </summary>
        public void PointersReleased()
        {
            numberOfPointersPressed = 0;
            distanceBetweenPoints = 0;
            angleBetweenVectors = 0;
            gestureDirection = GestureDirection.None;
        }

        /// <summary>
        /// Method sets startPoint's value
        /// </summary>
        /// <param name="startPoint">Gesture start point</param>
        public void SetStartPoint(Windows.Foundation.Point startPoint)
        {
            this.startPoint = startPoint;
        }
        /// <summary>
        /// Method sets endPoint's value
        /// </summary>
        /// <param name="endPoint">Gesture end point</param>
        public void SetEndPoint(Windows.Foundation.Point endPoint)
        {
            this.endPoint = endPoint;
        }
        /// <summary>
        /// Method calculates:
        /// *distance between start and end points
        /// *angle between (start and end points) vector and (start + [50, 0]) vector
        /// and sets value of <see cref="gestureDirection">gestureDirection</see>
        /// </summary>
        public void Calculate()
        {
            distanceBetweenPoints = CalculateDistance();
            angleBetweenVectors = CalculateAngle();


            if(numberOfPointersPressed >= 2)
            {
                gestureDirection = DetermineMotionDirectionDoubleTouch();
            }
            else if(numberOfPointersPressed == 1)
            {
                gestureDirection = DetermineMotionDirectionSingleTouch();
            }
            else
            {
                gestureDirection = GestureDirection.None;
            }
        }

        /// <returns>Method returns true if gesture direction is left</returns>
        public bool IsGestureDirectionLeft()
        {
            if (gestureDirection == GestureDirection.Left)
            {
                return true;
            }
            return false;
        }
        /// <returns>Method returns true if gesture direction is right</returns>
        public bool IsGestureDirectionRight()
        {
            if (gestureDirection == GestureDirection.Right)
            {
                return true;
            }
            return false;
        }
        /// <returns>Method returns true if gesture direction is up</returns>
        public bool IsGestureDirectionUp()
        {
            if (gestureDirection == GestureDirection.Up)
            {
                return true;
            }
            return false;
        }
        /// <returns>Method returns true if gesture direction is down</returns>
        public bool IsGestureDirectionDown()
        {
            if (gestureDirection == GestureDirection.Down)
            {
                return true;
            }
            return false;
        }
        /// <returns>Method returns true if gesture direction is top</returns>
        public bool IsGestureDirectionTop()
        {
            if (gestureDirection == GestureDirection.Top)
            {
                return true;
            }
            return false;
        }
        /// <returns>Method returns true if gesture direction is bottom</returns>
        public bool IsGestureDirectionBottom()
        {
            if (gestureDirection == GestureDirection.Bottom)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Method calculates distance between start and end points
        /// </summary>
        private double CalculateDistance()
        {
            return Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2) + Math.Pow(startPoint.Y - endPoint.Y, 2));
        }
        /// <summary>
        /// Method calculates angle between (start and end points) vector and (start + [50, 0]) vector
        /// </summary>
        private double CalculateAngle()
        {
            //Motion vector (startPoint -> endPoint)
            double xm = endPoint.X - startPoint.X;
            double ym = endPoint.Y - startPoint.Y;

            //Reference vector (startPoint -> startPoint(X+50, Y+0) 
            double xr = 50;
            double yr = 0;

            //Dot product of motion and reference vector
            double dotProduct = xm * xr + ym * yr;
            //Length of motion vector
            double lenm = Math.Sqrt(xm * xm + ym * ym);
            //Length of reference vector
            double lenr = Math.Sqrt(xr * xr + yr * yr);

            return Math.Acos(dotProduct / (lenm * lenr)) * 180 / Math.PI;
        }

        /// <summary>
        /// Method returns gesture direction, when player uses two fingers.
        /// </summary>
        /// <returns>Gesture Direction</returns>
        private GestureDirection DetermineMotionDirectionDoubleTouch()
        {
            if(distanceBetweenPoints >= minimumDistanceOfMotionRecognition)
            {
                //Gesture direction is right, using two fingers
                if (angleBetweenVectors < 75)
                {
                    return GestureDirection.Bottom;
                }
                //Gesture direction is left, using two fingers
                else if (angleBetweenVectors > 105)
                {
                    return GestureDirection.Top;
                }
            }
            return GestureDirection.None;
        }

        /// <summary>
        /// Method returns gesture direction, when player uses one finger.
        /// </summary>
        /// <returns>Gesture Direction</returns>
        private GestureDirection DetermineMotionDirectionSingleTouch()
        {
            if (distanceBetweenPoints >= minimumDistanceOfMotionRecognition)
            {
                if(angleBetweenVectors < 45)
                {
                    return GestureDirection.Right;
                }
                else if (135 < angleBetweenVectors)
                {
                    return GestureDirection.Left;
                }
                else
                {
                    if(startPoint.Y < endPoint.Y)
                    {
                        return GestureDirection.Down;
                    }
                    else if(startPoint.Y > endPoint.Y)
                    {
                        return GestureDirection.Up;
                    }
                }
            }
            return GestureDirection.None;
        }
    }
}
