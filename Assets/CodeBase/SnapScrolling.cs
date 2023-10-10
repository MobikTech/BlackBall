using System;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall
{
  public class SnapScrolling : MonoBehaviour
  {
    [Range(1, 50), Header("Number of balls")]
    public int ballCount;
    [Range(0, 100), Header("Distance between balls")]
    public float ballOffset;
    [Range(0, 20), Header("Snap speed")]
    public float snapSpeed;
    [Range(0, 5), Header("Scale offset")]
    public float scaleOffset;
    [Range(0, 20), Header("Scale speed")]
    public float scaleSpeed;
    [Header("Other objects")]
    public GameObject ballPrefab;
    public ScrollRect ScrollRect;

    private GameObject [] balls;
    private Vector2 [] ballPositions;
    private Vector2 [] ballScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private bool isScrolling;
    private int selectedBallID;

    private void Start()
    {
      contentRect = GetComponent<RectTransform>();
      balls = new GameObject[ballCount];
      ballPositions = new Vector2[ballCount];
      ballScale = new Vector2[ballCount];

      for (int i = 0; i < ballCount; i++)
      {
        balls[i] = Instantiate(ballPrefab, transform, false);

        if (i > 0)
        {
          balls[i].transform.localPosition = new Vector2(balls[i - 1].transform.localPosition.x + ballPrefab.GetComponent<RectTransform>().sizeDelta.x + ballOffset,
            balls[i].transform.localPosition.y);
        }

        ballPositions[i] = -balls[i].transform.localPosition;
      }
    }

    private void FixedUpdate()
    {
      if (contentRect.anchoredPosition.x >= ballPositions[0].x && !isScrolling || contentRect.anchoredPosition.x <= ballPositions[ballPositions.Length - 1].x && !isScrolling)
        ScrollRect.inertia = false;
      float nearestPos = float.MaxValue;

      for (int i = 0; i < ballCount; i++)
      {
        float distance = Math.Abs(contentRect.anchoredPosition.x - ballPositions[i].x);

        if (distance < nearestPos)
        {
          nearestPos = distance;
          selectedBallID = i;
        }

        float scale = Mathf.Clamp(1 / (distance / ballOffset) * scaleOffset, 0.5f, 1f);
        ballScale[i].x = Mathf.SmoothStep(balls[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
        ballScale[i].y = Mathf.SmoothStep(balls[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
        balls[i].transform.localScale = ballScale[i];
      }

      float scrollVelocity = Mathf.Abs(ScrollRect.velocity.x);

      if (scrollVelocity < 400 && !isScrolling)
        ScrollRect.inertia = false;

      if (isScrolling || scrollVelocity > 400)
        return;

      contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, ballPositions[selectedBallID].x, snapSpeed * Time.fixedDeltaTime);
      contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling (bool scroll)
    {
      isScrolling = scroll;

      if (scroll)
        ScrollRect.inertia = true;
    }
  }
}