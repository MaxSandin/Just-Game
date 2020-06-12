using RecognizerDollar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using WobbrockLib.Extensions;
using WobbrockLib.Types;

[System.Serializable]
public struct GesturePoint
{
    public double X;
    public double Y;
    public long Time;

    public GesturePoint(double x, double y, long ms)
    {
        X = x; Y = y; Time = ms;
    }
}

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<GesturePoint> points;
}

// Класс, использующий в тестовой сцене GestureScene для создания и определения жестов
public class HelperGestoreDetector : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public bool creatingMode = false;

    public List<Gesture> gestures;

    public TextMeshProUGUI text;

    private Recognizer _rec;
    private List<TimePointR> _points;
    void Start()
    {
        _rec = new Recognizer();
        foreach (var item in gestures)
            _rec.LoadGesture(item);

        text = GetComponentInChildren<TextMeshProUGUI>();
        _points = new List<TimePointR>(256);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        _points.Clear();
        _points.Add(new TimePointR(eventData.position.x, eventData.position.y, TimeEx.NowMs));

    }

    public void OnDrag(PointerEventData eventData)
    {
        _points.Add(new TimePointR(eventData.position.x, eventData.position.y, TimeEx.NowMs));
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if(creatingMode)
        {
            Gesture gest = new Gesture();
            gest.name = "New";
            gest.points = new List<GesturePoint>();
            foreach (var item in _points)
                gest.points.Add(new GesturePoint(item.X, item.Y, item.Time));

            gestures.Add(gest);
        }
        else
        {
            NBestList result = _rec.Recognize(_points, false); 
            string resultText = string.Format("{0} {1}", result.Score, result.Name);
            if (text)
                text.text = resultText;
            else
                Debug.Log(resultText);
        }

    }
}
