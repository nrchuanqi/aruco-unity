﻿using ArucoUnity.Plugin;
using UnityEngine;

namespace ArucoUnity
{
  /// \addtogroup aruco_unity_package
  /// \{

  namespace Objects
  {
    /// <summary>
    /// Describes a ChArUco board.
    /// </summary>
    public class ArucoCharucoBoard : ArucoBoard
    {
      // Editor fields

      [SerializeField]
      [Tooltip("Number of squares in the X direction.")]
      private int squaresNumberX;

      [SerializeField]
      [Tooltip("Number of squares in the Y direction.")]
      private int squaresNumberY;

      [SerializeField]
      [Tooltip("Side length of each square. In pixels for Creators. In meters for Trackers and Calibrators.")]
      private float squareSideLength;

      // Properties

      /// <summary>
      /// The number of squares in the X direction.
      /// </summary>
      public int SquaresNumberX
      {
        get { return squaresNumberX; }
        set
        {
          OnPropertyUpdating();
          squaresNumberX = value;
          OnPropertyUpdated();
        }
      }

      /// <summary>
      /// The number of squares in the Y direction.
      /// </summary>
      public int SquaresNumberY
      {
        get { return squaresNumberY; }
        set
        {
          OnPropertyUpdating();
          squaresNumberY = value;
          OnPropertyUpdated();
        }
      }

      /// <summary>
      /// The side length of each square. In pixels for Creators. In meters for Trackers and Calibrators.
      /// </summary>
      public float SquareSideLength
      {
        get { return squareSideLength; }
        set
        {
          OnPropertyUpdating();
          squareSideLength = value;
          OnPropertyUpdated();
        }
      }

      /// <summary>
      /// The list of the detected marker by the tracker the last frame.
      /// </summary>
      public Std.VectorPoint2f DetectedCorners { get; set; }

      /// <summary>
      /// The list of the ids of the detected marker by the tracker the last frame.
      /// </summary>
      public Std.VectorInt DetectedIds { get; set; }

      /// <summary>
      /// Is the transform of the board has been correctly estimated by the tracker the last frame.
      /// </summary>
      public bool ValidTransform { get; set; }

      // ArucoObject methods

      protected override void UpdateArucoHashCode()
      {
        ArucoHashCode = GetArucoHashCode(SquaresNumberX, SquaresNumberY, MarkerSideLength, SquareSideLength);
      }

      // ArucoBoard methods

      protected override void UpdateBoard()
      {
        ImageSize.Width = SquaresNumberX * (int)SquareSideLength + 2 * MarginsSize;
        ImageSize.Height = SquaresNumberY * (int)SquareSideLength + 2 * MarginsSize;

        AxisLength = 0.5f * (Mathf.Min(SquaresNumberX, SquaresNumberY) * SquareSideLength);

        Board = Aruco.CharucoBoard.Create(SquaresNumberX, SquaresNumberY, SquareSideLength, MarkerSideLength, Dictionary);
      }

      // Methods

      /// <summary>
      /// Computes the hash code of a ChAruco board.
      /// </summary>
      /// <param name="squaresNumberX">The number of squares in the X direction.</param>
      /// <param name="squaresNumberY">The number of squares in the Y direction.</param>
      /// <param name="markerSideLength">The side length of each marker.</param>
      /// <param name="squareSideLength">The side length of each square.</param>
      /// <returns>The calculated ArUco hash code.</returns>
      public static int GetArucoHashCode(int squaresNumberX, int squaresNumberY, float markerSideLength, float squareSideLength)
      {
        int hashCode = 17;
        hashCode = hashCode * 31 + typeof(ArucoCharucoBoard).GetHashCode();
        hashCode = hashCode * 31 + squaresNumberX;
        hashCode = hashCode * 31 + squaresNumberY;
        hashCode = hashCode * 31 + Mathf.RoundToInt(markerSideLength * 1000); // MarkerSideLength is not less than millimetres
        hashCode = hashCode * 31 + Mathf.RoundToInt(markerSideLength * 1000); // SquareSideLength is not less than millimetres
        return hashCode;
      }
    }
  }
  
  /// \} aruco_unity_package
}