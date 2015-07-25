using System;
using System.Windows;
using Windows.Foundation;
namespace _2048_3d_game.Controller
{

    class GestureController
    {
        private enum MoveType : int { Up = 0, Right = 1, Down = 2, Left = 3, Bottom = 4, Top = 5, None = -1 };

        private uint numberOfPointersPressed = 0;
        private Windows.Foundation.Point startPoint;
        private Windows.Foundation.Point endPoint;

        private double distanceBetweenPoints;
        private double angleBetweenVectors;
        private MoveType motionDirection;


        private double minimumDistanceOfMotionRecognition = 50;

        public GestureController()
        {

        }

        public void PointerPressed()
        {
            numberOfPointersPressed += 1;
        }
        public void PointersReleased()
        {
            numberOfPointersPressed = 0;

            distanceBetweenPoints = 0;
            angleBetweenVectors = 0;
            motionDirection = MoveType.None;
        }

        public void SetStartPoint(Windows.Foundation.Point startPoint)
        {
            this.startPoint = startPoint;
        }
        public void SetEndPoint(Windows.Foundation.Point endPoint)
        {
            this.endPoint = endPoint;
        }

        public void Calculate()
        {
            distanceBetweenPoints = DistanceBetweenPoints();
            angleBetweenVectors = AngleBetweenVectors();

            if(numberOfPointersPressed >= 2)
            {
                motionDirection = DetermineMotionDirectionFromDoubleTouch();
            }
            else if(numberOfPointersPressed == 1)
            {
                motionDirection = DetermineMotionDirectionFromSingleTouch();
            }
            else
            {
                motionDirection = MoveType.None;
            }
        }

        public bool IsMovedLeft()
        {
            if (motionDirection == MoveType.Left)
            {
                return true;
            }
            return false;
        }
        public bool IsMovedRight()
        {
            if (motionDirection == MoveType.Right)
            {
                return true;
            }
            return false;
        }
        public bool IsMovedUp()
        {
            if (motionDirection == MoveType.Up)
            {
                return true;
            }
            return false;
        }
        public bool IsMovedDown()
        {
            if (motionDirection == MoveType.Down)
            {
                return true;
            }
            return false;
        }
        public bool IsMovedTop()
        {
            if (motionDirection == MoveType.Top)
            {
                return true;
            }
            return false;
        }
        public bool IsMovedBottom()
        {
            if (motionDirection == MoveType.Bottom)
            {
                return true;
            }
            return false;
        }


        private double DistanceBetweenPoints()
        {
            return Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2) + Math.Pow(startPoint.Y - endPoint.Y, 2));
        }
        private double AngleBetweenVectors()
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

        private MoveType DetermineMotionDirectionFromDoubleTouch()
        {
            if(distanceBetweenPoints >= minimumDistanceOfMotionRecognition)
            {
                if (angleBetweenVectors < 75)
                {
                    return MoveType.Bottom;
                }
                else if (angleBetweenVectors > 105)
                {
                    return MoveType.Top;
                }
            }
            return MoveType.None;
        }

        private MoveType DetermineMotionDirectionFromSingleTouch()
        {
            if (distanceBetweenPoints >= minimumDistanceOfMotionRecognition)
            {
                if(angleBetweenVectors < 45)
                {
                    return MoveType.Right;
                }
                else if (135 < angleBetweenVectors)
                {
                    return MoveType.Left;
                }
                else
                {
                    if(startPoint.Y < endPoint.Y)
                    {
                        return MoveType.Down;
                    }
                    else if(startPoint.Y > endPoint.Y)
                    {
                        return MoveType.Up;
                    }
                }
            }
            return MoveType.None;
        }
    }
}
