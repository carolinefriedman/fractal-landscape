/*  Resources
 *  https://stevelosh.com/blog/2016/06/diamond-square/
 *  https://docs.unity3d.com/ScriptReference/Mesh.html/
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDS : MonoBehaviour
{
  // Number of divisions/faces
  public int mDivisions;
  // Size of terrain square
  public float mSize;
  // Maximum height of terrain
  public float mHeight;

  // Vector 3 array that holds vertices for terrain
  Vector3[] mVerts;
  int mVertCount;

  // Shader
  public Shader shader; 

  // Get size of the map
  // public float Size { get { return size; } }

  // Start is called before the first frame update
  void Start () {
    CreateTerrain();
  }

  // Update is called once per frame
  void CreateTerrain ()
  {
    // Number of vertices = 2^n + 1 in diamond square
    mVertCount = (mDivisions + 1) * (mDivisions + 1);
    mVerts = new Vector3[mVertCount];
    // Each vertice needs UV
    Vector2[] mUVs = new Vector2[mVertCount];
    // Number of triangles is (divisions * divisions) * 2 * 3 (in Unity)
    int[] mTris = new int[mDivisions * mDivisions * 6];

    // Half size of terrain
    float halfSize = mSize * 0.5f;
    // Division size
    float divisionSize = mSize / mDivisions;

    // Create Mesh
    Mesh mesh = new Mesh();
    GetComponent<MeshFilter>().mesh = mesh;

    int triOffset = 0;

    // Loop(s) for generating vertices (two loops for square)
    for (int i = 0; i <= mDivisions; i++) {
      for (int j = 0; j <= mDivisions; j++) {
        // 1-dimensional array to represent 2-dimensional matrix
        mVerts[i * (mDivisions + 1) + j] = new Vector3(-halfSize + j * divisionSize, 0.0f, halfSize - i * divisionSize);
        // Position [0, 0] → [0, 1], etc. to spread texture throughout terrain
        mUVs[i * (mDivisions + 1) + j] = new Vector2((float)i / mDivisions, (float)j / mDivisions);

        // Only need triangles where < divisions in rows and columns
        if (i < mDivisions && j < mDivisions) {
          int topLeft = i * (mDivisions + 1) + j;
          int bottomLeft = (i + 1) * (mDivisions + 1) + j;

          // Drawing first triangle ◥
          mTris[triOffset] = topLeft;
          mTris[triOffset + 1] = topLeft + 1;
          mTris[triOffset + 2] = bottomLeft + 1;

          // Drawing second triangle ◣
          mTris[triOffset + 3] = topLeft;
          mTris[triOffset + 4] = bottomLeft + 1;
          mTris[triOffset + 5] = bottomLeft;

          // Adding 6 after filling up entire square with triangles
          triOffset += 6;
        }
      }
    }

    // Set heights of corner edges of array to initial values
    mVerts[0].y = Random.Range(-mHeight, mHeight);
    mVerts[mDivisions].y = Random.Range(-mHeight, mHeight);
    mVerts[mVerts.Length - 1].y = Random.Range(-mHeight, mHeight);
    mVerts[mVerts.Length - 1 - mDivisions].y = Random.Range(-mHeight, mHeight);

    // Iterations for each square (diamond - square)
    int iterations = (int)Mathf.Log(mDivisions, 2);
    // Initialize to 1 square to start
    int numSquares = 1;
    // Initialize size of square with divisions
    int squareSize = mDivisions;

    // Current iteration
    for (int i = 0; i < iterations; i++) {
      int row = 0;
      // Two-by-two squares
      for (int j = 0; j < numSquares; j++) {
        int col = 0;
        for (int k = 0; k < numSquares; k++) {
          DiamondSquare(row, col, squareSize, mHeight);

          // Move over a square size
          col += squareSize;
        }
        // Move down a square size
        row += squareSize;
      }
      // Number of squares goes up by 2, size of square reduces by 2
      numSquares *= 2;
      squareSize /= 2;
      // Variable height
      mHeight *= 0.5f;
    }

    mesh.vertices = mVerts;
    mesh.uv = mUVs;
    mesh.triangles = mTris;

    mesh.RecalculateBounds();
    mesh.RecalculateNormals();
  }

  // Diamond Square function needs position, size, offset (height)
  void DiamondSquare(int row, int col, int size, float offset) {
    int halfSize = (int)(size * 0.5f);
    int topLeft = row * (mDivisions + 1) + col;
    int bottomLeft = (row + size) * (mDivisions + 1) + col;

    // Find midpoint between four corners
    int midpoint = (int)(row + halfSize) * (mDivisions + 1) + (int)(col + halfSize);
    // Average with random offset
    mVerts[midpoint].y = (mVerts[topLeft].y + mVerts[topLeft + size].y + mVerts[bottomLeft].y + mVerts[bottomLeft + size].y) * 0.25f + Random.Range(-offset, offset);

    //Finishing the square
    mVerts[topLeft + halfSize].y = (mVerts[topLeft].y + mVerts[topLeft + size].y + mVerts[midpoint].y) / 3 + Random.Range(-offset, offset);
    mVerts[midpoint - halfSize].y = (mVerts[topLeft].y + mVerts[bottomLeft].y + mVerts[midpoint].y) / 3 + Random.Range(-offset, offset);
    mVerts[midpoint + halfSize].y = (mVerts[topLeft + size].y + mVerts[bottomLeft + size].y + mVerts[midpoint].y) / 3 + Random.Range(-offset, offset);
    mVerts[bottomLeft + halfSize].y = (mVerts[bottomLeft].y + mVerts[bottomLeft + size].y + mVerts[midpoint].y) / 3 + Random.Range(-offset, offset);
  }
}
