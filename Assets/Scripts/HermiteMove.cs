using System;
using System.Collections.Generic;
using UnityEngine;

public class HermiteMove : MonoBehaviour
{
    /* Scale Function By http://www.digitalruby.com DigitalRuby.Tween */
    public enum EScaleFunc
    {
        Linear,
        QuadraticEaseIn,
        QuadraticEaseOut,
        QuadraticEaseInOut,
        CubicEaseIn,
        CubicEaseOut,
        CubicEaseInOut,
        QuarticEaseIn,
        QuarticEaseOut,
        QuarticEaseInOut,
        QuinticEaseIn,
        QuinticEaseOut,
        QuinticEaseInOut,
        SineEaseIn,
        SineEaseOut,
        SineEaseInOut
    }

    [SerializeField]
    private EScaleFunc _scaleFuncType = EScaleFunc.Linear;

    public EScaleFunc ScaleFuncType
    {
        get
        {
            return _scaleFuncType;
        }
        set
        {
            _scaleFuncType = value;
        }
    }

    private const float halfPi = Mathf.PI * 0.5f;

    /// <summary>
    /// A linear progress scale function.
    /// </summary>
    public static readonly Func<float, float> Linear = LinearFunc;

    private static float LinearFunc(float progress)
    {
        return progress;
    }

    /// <summary>
    /// A quadratic (x^2) progress scale function that eases in.
    /// </summary>
    public static readonly Func<float, float>
        QuadraticEaseIn = QuadraticEaseInFunc;

    private static float QuadraticEaseInFunc(float progress)
    {
        return EaseInPower(progress, 2);
    }

    /// <summary>
    /// A quadratic (x^2) progress scale function that eases out.
    /// </summary>
    public static readonly Func<float, float>
        QuadraticEaseOut = QuadraticEaseOutFunc;

    private static float QuadraticEaseOutFunc(float progress)
    {
        return EaseOutPower(progress, 2);
    }

    /// <summary>
    /// A quadratic (x^2) progress scale function that eases in and out.
    /// </summary>
    public static readonly Func<float, float>
        QuadraticEaseInOut = QuadraticEaseInOutFunc;

    private static float QuadraticEaseInOutFunc(float progress)
    {
        return EaseInOutPower(progress, 2);
    }

    /// <summary>
    /// A cubic (x^3) progress scale function that eases in.
    /// </summary>
    public static readonly Func<float, float> CubicEaseIn = CubicEaseInFunc;

    private static float CubicEaseInFunc(float progress)
    {
        return EaseInPower(progress, 3);
    }

    /// <summary>
    /// A cubic (x^3) progress scale function that eases out.
    /// </summary>
    public static readonly Func<float, float> CubicEaseOut = CubicEaseOutFunc;

    private static float CubicEaseOutFunc(float progress)
    {
        return EaseOutPower(progress, 3);
    }

    /// <summary>
    /// A cubic (x^3) progress scale function that eases in and out.
    /// </summary>
    public static readonly Func<float, float>
        CubicEaseInOut = CubicEaseInOutFunc;

    private static float CubicEaseInOutFunc(float progress)
    {
        return EaseInOutPower(progress, 3);
    }

    /// <summary>
    /// A quartic (x^4) progress scale function that eases in.
    /// </summary>
    public static readonly Func<float, float> QuarticEaseIn = QuarticEaseInFunc;

    private static float QuarticEaseInFunc(float progress)
    {
        return EaseInPower(progress, 4);
    }

    /// <summary>
    /// A quartic (x^4) progress scale function that eases out.
    /// </summary>
    public static readonly Func<float, float>
        QuarticEaseOut = QuarticEaseOutFunc;

    private static float QuarticEaseOutFunc(float progress)
    {
        return EaseOutPower(progress, 4);
    }

    /// <summary>
    /// A quartic (x^4) progress scale function that eases in and out.
    /// </summary>
    public static readonly Func<float, float>
        QuarticEaseInOut = QuarticEaseInOutFunc;

    private static float QuarticEaseInOutFunc(float progress)
    {
        return EaseInOutPower(progress, 4);
    }

    /// <summary>
    /// A quintic (x^5) progress scale function that eases in.
    /// </summary>
    public static readonly Func<float, float> QuinticEaseIn = QuinticEaseInFunc;

    private static float QuinticEaseInFunc(float progress)
    {
        return EaseInPower(progress, 5);
    }

    /// <summary>
    /// A quintic (x^5) progress scale function that eases out.
    /// </summary>
    public static readonly Func<float, float>
        QuinticEaseOut = QuinticEaseOutFunc;

    private static float QuinticEaseOutFunc(float progress)
    {
        return EaseOutPower(progress, 5);
    }

    /// <summary>
    /// A quintic (x^5) progress scale function that eases in and out.
    /// </summary>
    public static readonly Func<float, float>
        QuinticEaseInOut = QuinticEaseInOutFunc;

    private static float QuinticEaseInOutFunc(float progress)
    {
        return EaseInOutPower(progress, 5);
    }

    /// <summary>
    /// A sine progress scale function that eases in.
    /// </summary>
    public static readonly Func<float, float> SineEaseIn = SineEaseInFunc;

    private static float SineEaseInFunc(float progress)
    {
        return Mathf.Sin(progress * halfPi - halfPi) + 1;
    }

    /// <summary>
    /// A sine progress scale function that eases out.
    /// </summary>
    public static readonly Func<float, float> SineEaseOut = SineEaseOutFunc;

    private static float SineEaseOutFunc(float progress)
    {
        return Mathf.Sin(progress * halfPi);
    }

    /// <summary>
    /// A sine progress scale function that eases in and out.
    /// </summary>
    public static readonly Func<float, float> SineEaseInOut = SineEaseInOutFunc;

    private static float SineEaseInOutFunc(float progress)
    {
        return (Mathf.Sin(progress * Mathf.PI - halfPi) + 1) / 2;
    }

    private static float EaseInPower(float progress, int power)
    {
        return Mathf.Pow(progress, power);
    }

    private static float EaseOutPower(float progress, int power)
    {
        int sign = power % 2 == 0 ? -1 : 1;
        return (sign * (Mathf.Pow(progress - 1, power) + sign));
    }

    private static float EaseInOutPower(float progress, int power)
    {
        progress *= 2.0f;
        if (progress < 1)
        {
            return Mathf.Pow(progress, power) / 2.0f;
        }
        else
        {
            int sign = power % 2 == 0 ? -1 : 1;
            return (sign / 2.0f * (Mathf.Pow(progress - 2, power) + sign * 2));
        }
    }

    private Func<float, float> ScaleFunc
    {
        get
        {
            switch (_scaleFuncType)
            {
                case EScaleFunc.Linear:
                    return Linear;
                case EScaleFunc.QuadraticEaseIn:
                    return QuadraticEaseInFunc;
                case EScaleFunc.QuadraticEaseOut:
                    return QuadraticEaseOutFunc;
                case EScaleFunc.QuadraticEaseInOut:
                    return QuadraticEaseInOutFunc;
                case EScaleFunc.CubicEaseIn:
                    return CubicEaseInFunc;
                case EScaleFunc.CubicEaseOut:
                    return CubicEaseOutFunc;
                case EScaleFunc.CubicEaseInOut:
                    return CubicEaseInOutFunc;
                case EScaleFunc.QuarticEaseIn:
                    return QuarticEaseInFunc;
                case EScaleFunc.QuarticEaseOut:
                    return QuarticEaseOutFunc;
                case EScaleFunc.QuarticEaseInOut:
                    return QuarticEaseInOutFunc;
                case EScaleFunc.QuinticEaseIn:
                    return QuinticEaseInFunc;
                case EScaleFunc.QuinticEaseOut:
                    return QuinticEaseOutFunc;
                case EScaleFunc.QuinticEaseInOut:
                    return QuinticEaseInOutFunc;
                case EScaleFunc.SineEaseIn:
                    return SineEaseInFunc;
                case EScaleFunc.SineEaseOut:
                    return SineEaseOutFunc;
                case EScaleFunc.SineEaseInOut:
                    return SineEaseInOutFunc;
            }

            return Linear;
        }
    }

    public enum EMoveType
    {
        World,
        Local,
        UI
    }

    [SerializeField]
    private EMoveType _moveType = EMoveType.World;

    public EMoveType MoveType
    {
        get
        {
            return _moveType;
        }
        set
        {
            _moveType = value;
        }
    }

    private enum EMoveState
    {
        None,
        Wait,
        Moving
    }

    private EMoveState _state = EMoveState.None;

    private float _duration;

    private Action<HermiteMove> _completion;

    private Action<HermiteMove> _afterWait;

    private float _timePassed;

    private float _startDelay;

    private List<Vector3> _points;

    public bool Moveable => _points != null && _points.Count >= 2;

    public void Stop()
    {
        _timePassed = 0;
        _state = EMoveState.None;
    }

    public Vector3 PointOnHermite(IList<Vector3> pointList, float progress_)
    {
        var progress = ScaleFunc.Invoke(progress_);

        if (pointList.Count == 2)
        {
            return pointList[0] + (progress * (pointList[1] - pointList[0]));
        }

        int idx = 0;
        float segmentCount = pointList.Count - 1;
        for (var i = 0; i < segmentCount; i++)
        {
            if (
                progress >= i / segmentCount &&
                progress < (i + 1) / segmentCount
            )
            {
                idx = i;
                break;
            }
        }

        var t = (progress - (idx / segmentCount)) * segmentCount;
        if (t < 0) t = 0;
        if (t > 1) t = 1;

        // determine control points of segment
        var p0 = pointList[idx];
        var p1 = pointList[idx + 1];

        Vector3 m0;
        if (idx > 0)
        {
            m0 = (pointList[idx + 1] - pointList[idx - 1]) * 0.5f;
        }
        else
        {
            m0 = pointList[idx + 1] - pointList[idx];
        }

        Vector3 m1;
        if (idx < pointList.Count - 2)
        {
            m1 = (pointList[idx + 2] - pointList[idx]) * 0.5f;
        }
        else
        {
            m1 = pointList[idx + 1] - pointList[idx];
        }

        float t2 = t * t;
        float t3 = t2 * t;
        var position =
            (2.0f * t3 - 3.0f * t2 + 1.0f) * p0 +
            (t3 - 2.0f * t2 + t) * m0 +
            (-2.0f * t3 + 3.0f * t2) * p1 +
            (t3 - t2) * m1;

        return position;
    }

    public void SetupPoints(
        Vector3 p0,
        Vector3 p1,
        Vector3? p2 = null,
        Vector3? p3 = null,
        Vector3? p4 = null
    )
    {
        _state = EMoveState.None;

        if (_points == null) _points = new List<Vector3>();
        _points.Clear();
        _points.Add (p0);
        _points.Add (p1);
        if (p2 != null) _points.Add(p2.Value);
        if (p3 != null) _points.Add(p3.Value);
        if (p4 != null) _points.Add(p4.Value);
    }

    public void Run(
        IList<Vector3> points,
        float duration,
        Action<HermiteMove> completion = null,
        float startDelay = 0,
        Action<HermiteMove> afterWait = null
    )
    {
        if (_points == null) _points = new List<Vector3>();
        _points.Clear();
        _points.AddRange (points);
        _timePassed = 0;
        _duration = duration;
        _completion = completion;
        _startDelay = startDelay;
        _afterWait = afterWait;
        _state = EMoveState.Wait;
    }

    public void Run(
        Vector3 point0,
        Vector3 point1,
        Vector3 point2,
        float duration,
        Action<HermiteMove> completion = null,
        float startDelay = 0,
        Action<HermiteMove> afterWait = null
    )
    {
        SetupPoints (point0, point1, point2);
        _timePassed = 0;
        _duration = duration;
        _completion = completion;
        _startDelay = startDelay;
        _afterWait = afterWait;
        _state = EMoveState.Wait;
    }

    public void Run(
        Vector3 point0,
        Vector3 point1,
        float duration,
        Action<HermiteMove> completion = null,
        float startDelay = 0,
        Action<HermiteMove> afterWait = null
    )
    {
        SetupPoints (point0, point1);
        _timePassed = 0;
        _duration = duration;
        _completion = completion;
        _startDelay = startDelay;
        _afterWait = afterWait;
        _state = EMoveState.Wait;
    }

    public bool UpdatePosition(float progress)
    {
        if (progress <= 1)
        {
            _updatePos = PointOnHermite(_points, progress);
        }
        else
        {
            _updatePos = _points[_points.Count - 1];
        }

        UpdatePosition (_updatePos);

        return (progress >= 1);
    }

    public void UpdatePosition(Vector3 position)
    {
        switch (_moveType)
        {
            case EMoveType.World:
                transform.position = position;
                break;
            case EMoveType.Local:
                transform.localPosition = position;
                break;
            case EMoveType.UI:
                (transform as RectTransform).anchoredPosition = position;
                break;
            default:
                break;
        }
    }

    private Vector3 _updatePos;

    private void Update()
    {
        var dt = Time.deltaTime;

        switch (_state)
        {
            case EMoveState.Wait:
                {
                    _timePassed += dt;
                    if (_timePassed >= _startDelay)
                    {
                        _timePassed = 0;
                        _afterWait?.Invoke(this);
                        _state = EMoveState.Moving;
                    }
                }
                break;
            case EMoveState.Moving:
                {
                    _timePassed += dt;
                    bool isComplete = UpdatePosition(_timePassed / _duration);
                    if (isComplete)
                    {
                        _state = EMoveState.None;
                        _completion?.Invoke(this);
                    }
                }
                break;
            case EMoveState.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
